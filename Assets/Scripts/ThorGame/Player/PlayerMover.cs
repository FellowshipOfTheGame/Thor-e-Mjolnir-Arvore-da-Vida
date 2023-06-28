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
        [SerializeField] private LayerMask collisionMask;
        [SerializeField] private GroundChecker groundChecker;

        public bool CanJump => groundChecker.IsGrounded;
        
        public Rigidbody2D Rigidbody { get; private set; }
        public CapsuleCollider2D Collider { get; private set; }
        public Vector2 Velocity { get; private set; }
        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Collider = GetComponent<CapsuleCollider2D>();
        }

        public void Jump()
        {
            if (!CanJump) return;
            Debug.Log("jump!");
            //ApplyMovement(Vector2.up * jumpForce);
            Velocity += Vector2.up * jumpForce;
        }

        private Vector2 CalculateHorizontalMovementDirection()
        {
            if (!groundChecker.IsGrounded) return Vector2.right;
            Vector2 groundNormal = groundChecker.GroundHit.normal;
            //Vector3.Cross(groundNormal, Vector3.forward)
            return Vector3.ProjectOnPlane(Vector3.right, groundNormal).normalized;
        }
        public void MoveHorizontally(float movement)
        {
            Vector2 direction = CalculateHorizontalMovementDirection();
            //ApplyMovement(direction * movement);
            Velocity += direction * movement;
        }
        
        private void FixedUpdate()
        {
            groundChecker.UpdateGrounded(Collider.bounds);
            if (!groundChecker.IsGrounded)
            {
                //ApplyMovement(Vector2.down * gravity);
                Velocity += Vector2.down * gravity;
            }

            if (Velocity != Vector2.zero)
            {
                ApplyMovement(Velocity);
                Velocity = Vector2.zero;
            }
        }

        private readonly List<RaycastHit2D> _castHits = new();

        private Vector2 GetCollidedPosition(RaycastHit2D hit, Vector2 movement)
        {
            Vector2 padding = hit.normal * collisionPadding;
            Vector2 projectedMovement = Vector3.ProjectOnPlane(movement, hit.normal);
            return hit.centroid + padding + projectedMovement;
        }
        
        private void ApplyMovement(Vector2 movement)
        {
            _castHits.Clear();
            ContactFilter2D filter = new ContactFilter2D() {layerMask =  collisionMask};
            int hitCount = Rigidbody.Cast(movement.normalized, filter, _castHits, movement.magnitude);

            Vector2 finalPos = Rigidbody.position + movement;
            if (hitCount > 0)
            {
                finalPos = GetCollidedPosition(_castHits[0], movement);
            }
            Debug.DrawRay(Rigidbody.position, Rigidbody.position + movement, Color.red);
            Debug.DrawLine(Rigidbody.position, finalPos, Color.magenta);
            Rigidbody.MovePosition(finalPos);
        }

        //DEBUG
        private void Update() => groundChecker.DEBUG_Draw(Collider.bounds);
    }
}