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
            else if (_targetMovement.y < 0)
            {
                _targetMovement.y = 0;
            }
            ApplyMovement();
            _targetMovement.x = Mathf.MoveTowards(_targetMovement.x, 0, deceleration);
        }

        private void ApplyMovement()
        {
            Vector2 movement = CalculateHorizontalMovement(Vector2.right * _targetMovement.x) +
                               Vector2.up * _targetMovement.y;
            
            List<RaycastHit2D> castHits = new();
            ContactFilter2D filter = new ContactFilter2D() {layerMask =  collisionMask};
            int hitCount = Rigidbody.Cast(movement.normalized, filter, castHits, movement.magnitude);

            Vector2 finalPos = Rigidbody.position + movement;
            if (hitCount > 0)
            {
                var hit = castHits[0];
                finalPos = hit.centroid + hit.normal * collisionPadding;
                _targetMovement = Vector3.ProjectOnPlane(movement,  hit.normal);
            }
            Rigidbody.MovePosition(finalPos);
        }

        //DEBUG
        private void Update() => groundChecker.DEBUG_Draw(Collider.bounds);
    }
}