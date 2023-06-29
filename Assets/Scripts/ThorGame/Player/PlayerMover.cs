using System.Collections.Generic;
using UnityEngine;

namespace ThorGame.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float walkVelocity;
        [SerializeField] private float gravity;
        [SerializeField] private float jumpForce;
        [SerializeField] private float collisionPadding = 0.025f;
        [SerializeField] private float deceleration;
        [SerializeField] private LayerMask collisionMask;
        [SerializeField] private GroundChecker groundChecker;

        public GroundChecker GroundChecker => groundChecker;

        public bool CanJump => groundChecker.IsGrounded;
        
        public Rigidbody2D Rigidbody { get; private set; }
        public CapsuleCollider2D Collider { get; private set; }

        private Vector2 _targetMovement;
        
        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Collider = GetComponent<CapsuleCollider2D>();
        }

        public void Jump()
        {
            if (!CanJump) return;
            Debug.Log("jump!");
            _targetMovement.y = jumpForce;
        }

        private Vector2 CalculateHorizontalMovement(Vector2 movement)
        {
            if (!groundChecker.IsGrounded) return movement;
            Vector2 groundNormal = groundChecker.DirectionalHit(movement.x).normal;
            Debug.DrawRay(Rigidbody.position, Vector3.ProjectOnPlane(movement, groundNormal), Color.blue);
            return Vector3.ProjectOnPlane(movement, groundNormal);
        }
        public void SetHorizontalMovement(float movement)
        {
            _targetMovement.x = movement * walkVelocity;
        }
        
        private void FixedUpdate()
        {
            groundChecker.UpdateGrounded(Collider.bounds);
            if (!groundChecker.IsGrounded)
            {
                _targetMovement.y -= gravity;
            }
            ApplyMovement();
            _targetMovement.x = Mathf.MoveTowards(_targetMovement.x, 0, deceleration);
        }

        private readonly List<RaycastHit2D> _castHits = new();

        private Vector2 GetCollidedPosition(RaycastHit2D hit, Vector2 movement)
        {
            /*Vector2 padding = hit.normal * collisionPadding;
            Vector2 projectedMovement = Vector3.ProjectOnPlane(movement, hit.normal);
            return hit.centroid + padding + projectedMovement;*/
            return hit.centroid + hit.normal * collisionPadding;
        }
        
        private void ApplyMovement()
        {
            Vector2 movement = CalculateHorizontalMovement(Vector2.right * _targetMovement.x) +
                               Vector2.up * _targetMovement.y;
            
            _castHits.Clear();
            ContactFilter2D filter = new ContactFilter2D() {layerMask =  collisionMask};
            int hitCount = Rigidbody.Cast(movement.normalized, filter, _castHits, movement.magnitude);

            Vector2 finalPos = Rigidbody.position + movement;
            if (hitCount > 0)
            {
                finalPos = GetCollidedPosition(_castHits[0], movement);
                _targetMovement = Vector3.ProjectOnPlane(movement, _castHits[0].normal);
            }
            Debug.DrawRay(Rigidbody.position, movement, Color.red);
            Debug.DrawLine(Rigidbody.position, finalPos, Color.magenta);
            Rigidbody.MovePosition(finalPos);
        }

        //DEBUG
        private void Update() => groundChecker.DEBUG_Draw(Collider.bounds);
    }
}