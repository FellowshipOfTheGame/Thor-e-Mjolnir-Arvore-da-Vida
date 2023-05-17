using System;
using System.Linq;
using ThorGame.Player.HammerControls.Modes;
using ThorGame.Player.HammerControls.ModeSet;
using UnityEngine;

namespace ThorGame.Player.HammerControls
{
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
        
        [SerializeField] private GameObject strap;
        [SerializeField] private AnchoredJoint2D hammerStrapJoint;
        [SerializeField] private AnchoredJoint2D hammerFixedJoint;
        
        public Rigidbody2D Rigidbody { get; private set; }

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

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            AttachmentMode = Attachment.Held;
        }

        private void Update()
        {
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