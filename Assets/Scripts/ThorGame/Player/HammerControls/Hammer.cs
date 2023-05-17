using System;
using ThorGame.Player.HammerControls.Modes;
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
        
        [SerializeField] private GameObject strap;
        [SerializeField] private AnchoredJoint2D hammerStrapJoint;
        [SerializeField] private AnchoredJoint2D hammerFixedJoint;
        
        [SerializeField]
        private HammerMode _strapMode, _straplessMode, _thrownMode;
        private HammerMode _currentMode;
        
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
            
            //_hammerMode = Instantiate(_hammerMode);
            _strapMode = Instantiate(_strapMode);
            _straplessMode = Instantiate(_straplessMode);
            
            //_hammerMode.Begin(this);
            StartMode(_straplessMode);
        }

        private void Update()
        {
            if (_currentMode)
            {
                _currentMode.Tick(this);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                StartMode(_currentMode == _strapMode ? _straplessMode : _strapMode);
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                StartMode(_thrownMode);
            }
        }

        private void StartMode(HammerMode mode)
        {
            if (_currentMode) _currentMode.End(this);
            _currentMode = mode;
            mode.Begin(this);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out IHittable hittable))
            {
                _currentMode.OnCollide(this, hittable);
            }
        }
    }
}