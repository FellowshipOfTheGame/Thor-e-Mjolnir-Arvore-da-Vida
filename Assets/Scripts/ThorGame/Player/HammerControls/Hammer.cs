using System;
using System.Linq;
using ThorGame.Player.HammerControls.Modes;
using UnityEngine;

namespace ThorGame.Player.HammerControls
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Hammer : MonoBehaviour
    {
        public enum Attachment
        {
            Held,
            Strap,
            Free
        }
        
        [SerializeField] private HammerMode[] unlockedModes;
        public bool IsUnlocked(HammerMode mode) => unlockedModes.Contains(mode);

        [SerializeField] private Vector2 headOffset;

        [Header("Movement")]
        [SerializeField] private float flyVelocity;
        [SerializeField] private float maxVelocity;
        [SerializeField] private float recallDistance;
        [SerializeField] private bool faceVelocity = true;
        [SerializeField] [Range(0f, 360f)] private float upAngle;
        [SerializeField] private float heldGravityScale = 1;
        [SerializeField] private float freeGravityScale = 1;

        [Header("Damage")] 
        [SerializeField] private float minimumSpeedToHit;
        [SerializeField] private int heldDamage;
        [SerializeField] private int freeDamage;

        [Header("Constraints")]
        [SerializeField] private GameObject strap;
        [SerializeField] private AnchoredJoint2D hammerStrapJoint;
        [SerializeField] private AnchoredJoint2D hammerFixedJoint;

        public Rigidbody2D Rigidbody { get; private set; }
        private Transform _ogParent;
        private bool _recalling;
        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Rigidbody.centerOfMass = headOffset;
            _ogParent = transform.parent;
            AttachmentMode = Attachment.Held;
        }

        private void FixedUpdate()
        {
            if (AttachmentMode == Attachment.Free)
            {
                Vector2 vel = Rigidbody.velocity;
                if (vel != Vector2.zero && faceVelocity)
                {
                    transform.up = vel.normalized;
                }

                float velocityMagnitude = Rigidbody.velocity.magnitude;
                if (maxVelocity > 0 && velocityMagnitude > maxVelocity)
                {
                    Rigidbody.velocity = Rigidbody.velocity * maxVelocity / velocityMagnitude;
                }
            }
        }

        public void Recall()
        {
            _recalling = true;
            FlyTowards(OriginPosition);
            if ((Rigidbody.position - OriginPosition).sqrMagnitude <= recallDistance * recallDistance)
            {
                AttachmentMode = Attachment.Held;
            }
        }
        private void Update() => _recalling = false;
        
        public void FlyTowards(Vector2 target)
        {
            Vector2 dir = (target - Rigidbody.position).normalized;
            Rigidbody.velocity = dir * flyVelocity;
        }

        public Vector2 OriginPosition => strap.transform.position;

        private Attachment _attachment;
        public Attachment AttachmentMode
        {
            get => _attachment;
            set
            {
                _attachment = value;
                switch (_attachment)
                {
                    case Attachment.Held:
                    {
                        strap.SetActive(false);
                        hammerStrapJoint.enabled = false;
                        hammerFixedJoint.enabled = true;
                        Rigidbody.gravityScale = heldGravityScale;

                        transform.parent = _ogParent;
                        Transform strapTransform = strap.transform;
                        Transform forearm = transform.parent;
                        transform.position = strapTransform.position;
                        transform.up = forearm.up;
                        break;
                    }
                    case Attachment.Strap:
                    {
                        strap.SetActive(true);
                        hammerStrapJoint.enabled = true;
                        hammerFixedJoint.enabled = false;
                        Rigidbody.gravityScale = heldGravityScale;
                        
                        transform.parent = _ogParent;
                        Transform strapTransform = strap.transform;
                        Vector2 strapPos = strapTransform.position;
                        Vector2 strapDir = strapTransform.up;
                        Vector2 strapEnd = strapPos + strapDir * strapTransform.localScale.y;
                        transform.position = strapEnd;
                        transform.up = strapDir;
                        break;
                    }
                    case Attachment.Free:
                    {
                        strap.SetActive(false);
                        hammerStrapJoint.enabled = false;
                        hammerFixedJoint.enabled = false;
                        Rigidbody.gravityScale = freeGravityScale;
                        transform.parent = null;
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.TryGetComponent<IHittable>(out var hittable)) return;
            if (hittable.RequireMinSpeed && !_recalling &&
                Rigidbody.velocity.sqrMagnitude < minimumSpeedToHit * minimumSpeedToHit)
                return;
            int damage = _attachment == Attachment.Free ? freeDamage : heldDamage;
            hittable.Hit(Rigidbody.position, Rigidbody.velocity, damage);
        }

#if UNITY_EDITOR
        [UnityEditor.CustomEditor(typeof(Hammer))]
        private class HammerEditor : UnityEditor.Editor
        {
            private void OnSceneGUI()
            {
                if (target is not Hammer hammer) return;
                UnityEditor.Handles.color = Color.blue;
                Vector3 com = hammer.transform.position +
                              hammer.transform.TransformDirection(hammer.headOffset); 
                UnityEditor.Handles.DrawSolidDisc(com, Vector3.forward, .1f);
                
                float rad = Mathf.Deg2Rad * hammer.upAngle;
                Vector3 upDir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
                UnityEditor.Handles.DrawLine(hammer.transform.position, hammer.transform.position + upDir);
                UnityEditor.Handles.ArrowHandleCap(0, hammer.transform.position + upDir,
                    Quaternion.LookRotation(upDir), 
                    2f, EventType.Repaint);
            }
        }
        #endif
    }
}