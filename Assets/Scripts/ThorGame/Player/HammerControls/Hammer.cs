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
        [SerializeField] private float maxVelocity;
        [SerializeField] private float recallDistance;
        
        [Header("Constraints")]
        [SerializeField] private GameObject strap;
        [SerializeField] private AnchoredJoint2D hammerStrapJoint;
        [SerializeField] private AnchoredJoint2D hammerFixedJoint;
        
        public Rigidbody2D Rigidbody { get; private set; }
        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Rigidbody.centerOfMass = headOffset;
            AttachmentMode = Attachment.Held;
        }

        private void FixedUpdate()
        {
            if (AttachmentMode == Attachment.Free)
            {
                Vector2 vel = Rigidbody.velocity;
                if (vel != Vector2.zero)
                {
                    Rigidbody.SetRotation(Quaternion.LookRotation(vel.normalized, Vector3.back));
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
            FlyTowards(OriginPosition);
            //Talvez valha a pena pegar de volta sempre que chegar perto da mão, e não apenas enquanto está recallando
            if ((Rigidbody.position - OriginPosition).sqrMagnitude <= recallDistance * recallDistance)
            {
                AttachmentMode = Attachment.Held;
            }
        }
        
        public void FlyTowards(Vector2 target)
        {
            Vector2 dir = (target - Rigidbody.position).normalized;
            Rigidbody.velocity = dir * maxVelocity;
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
                        
                        Transform strapTransform = hammerStrapJoint.transform;
                        Transform forearm = transform.parent;
                        Vector2 strapPos = strapTransform.position;
                        transform.position = strapPos;
                        transform.up = forearm.up;
                        break;
                    }
                    case Attachment.Strap:
                    {
                        strap.SetActive(true);
                        hammerStrapJoint.enabled = true;
                        hammerFixedJoint.enabled = false;
                        
                        Transform strapTransform = hammerStrapJoint.transform;
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
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out IHittable hittable))
            {
                //ModeSetTree.
                //_currentMode.OnCollide(this, hittable);
            }
        }
    }
}