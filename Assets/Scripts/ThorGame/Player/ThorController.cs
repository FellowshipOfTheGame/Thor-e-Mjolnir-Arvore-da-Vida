using UnityEngine;

namespace ThorGame.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
    public class ThorController : MonoBehaviour
    {
        [SerializeField] private string moveAxis = "Horizontal";
        [SerializeField] private GroundChecker groundChecker;
    
        public Rigidbody2D Rigidbody { get; private set; }
        public CapsuleCollider2D Collider { get; private set; }
        
        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Collider = GetComponent<CapsuleCollider2D>();
        }
    
        private float _horizontalMovement;
        private bool _jumpQueued;
        private void Update()
        {
            _horizontalMovement = Input.GetAxis(moveAxis);
            
            
            
            groundChecker.DEBUG_Draw(Collider.bounds);
            Debug.DrawRay(transform.position, Vector3.right * _horizontalMovement, Color.yellow);
        }
    
        private void FixedUpdate()
        {
            groundChecker.UpdateGrounded(Collider.bounds);
        }
    }
}
