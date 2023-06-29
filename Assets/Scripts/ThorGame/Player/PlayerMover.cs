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

        public float GravityMultiplier { get; set; } = 1;

        public GroundChecker GroundChecker => groundChecker;

        public bool CanJump => groundChecker.IsGrounded;
        
        public Rigidbody2D Rigidbody { get; private set; }
        public CapsuleCollider2D Collider { get; private set; }

        private Vector2 _velocity;
        public Vector2 Velocity => _velocity;
        
        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Collider = GetComponent<CapsuleCollider2D>();
        }

        public void Jump()
        {
            if (!CanJump) return;
            Debug.Log("jump!");
            _velocity.y = jumpForce;
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
            _velocity.x = movement * walkVelocity;
        }
        
        private void FixedUpdate()
        {
            groundChecker.UpdateGrounded(Collider.bounds);
            if (!groundChecker.IsGrounded)
            {
                _velocity.y -= gravity * GravityMultiplier;
            }
            else if (_velocity.y < 0)
            {
                _velocity.y = 0;
            }
            ApplyMovement();
            _velocity.x = Mathf.MoveTowards(_velocity.x, 0, deceleration);
        }

        private void ApplyMovement()
        {
            Vector2 movement = CalculateHorizontalMovement(Vector2.right * _velocity.x) +
                               Vector2.up * _velocity.y;
            
            List<RaycastHit2D> castHits = new();
            ContactFilter2D filter = new ContactFilter2D() {layerMask =  collisionMask};
            int hitCount = Rigidbody.Cast(movement.normalized, filter, castHits, movement.magnitude);

            Vector2 finalPos = Rigidbody.position + movement;
            if (hitCount > 0)
            {
                var hit = castHits[0];
                finalPos = hit.centroid + hit.normal * collisionPadding;
                _velocity = Vector3.ProjectOnPlane(movement,  hit.normal);
            }
            Rigidbody.MovePosition(finalPos);
        }

        //DEBUG
        private void Update() => groundChecker.DEBUG_Draw(Collider.bounds);
    }
}