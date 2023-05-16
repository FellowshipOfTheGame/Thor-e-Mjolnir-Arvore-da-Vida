using ThorGame.Player.HammerControls.Modes;
using UnityEngine;

namespace ThorGame.Player.HammerControls
{
    public class Hammer : MonoBehaviour
    {
        [SerializeField] private GameObject strap;
        [SerializeField] private AnchoredJoint2D hammerStrapJoint;
        [SerializeField] private AnchoredJoint2D hammerFixedJoint;
        
        [SerializeField]
        private HammerMode _strapMode, _straplessMode;
        private HammerMode _currentMode;
        
        public Rigidbody2D Rigidbody { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            SetStrap(false);
            
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
        }

        private void StartMode(HammerMode mode)
        {
            if (_currentMode) _currentMode.End(this);
            _currentMode = mode;
            mode.Begin(this);
        }
        public void SetStrap(bool active)
        {
            strap.SetActive(active);
            
            var strapTransform = hammerStrapJoint.transform;
            if (active)
            {
                Vector2 strapPos = strapTransform.position;
                Vector2 strapDir = strapTransform.up;
                Vector2 strapEnd = strapPos + strapDir * strapTransform.localScale.y;
                //hammerStrapJoint.connectedAnchor = strapEnd - strapPos;
                transform.position = strapEnd;
                transform.up = strapDir;
            }
            else
            {
                Transform forearm = transform.parent;
                Vector2 strapPos = strapTransform.position;
                //hammerFixedJoint.connectedAnchor  = strapPos - (Vector2)forearm.position;
                transform.position = strapPos;
                transform.up = forearm.up;
            }
            
            hammerStrapJoint.enabled = active;
            hammerFixedJoint.enabled = !active;
        }
    }
}