using System;
using UnityEngine;

namespace ThorGame.Player
{
    [RequireComponent(typeof(PlayerMover))]
    public class ThorController : MonoBehaviour
    {
        [SerializeField] private string moveAxis = "Horizontal";
        [SerializeField] private KeyCode jumpKey;
        [SerializeField] private Timer jumpQueueTimer;
        [SerializeField] private Timer jumpCooldownTimer;

        [Header("Better Jump")] 
        [SerializeField] private float risingMultiplier = 1;
        [SerializeField] private float risingPressedMultiplier = 0.6f;
        [SerializeField] private float fallingMultiplier = 1.5f;
        [SerializeField] private float fallingPressedMultiplier = 0.8f;

        public PlayerMover Mover { get; private set; }
        private void Awake()
        {
            Mover = GetComponent<PlayerMover>();
        }

        public float HorizontalMovement { get; private set; }

        private bool _jumpQueued;
        private void Update()
        {
            HorizontalMovement = Input.GetAxis(moveAxis);

            bool jumpPressed = Input.GetKey(jumpKey);
            if (jumpCooldownTimer.Tick() && jumpPressed)
            {
                _jumpQueued = true;
                jumpQueueTimer.Reset();
            }
            else if (_jumpQueued && jumpQueueTimer.Tick())
            {
                _jumpQueued = false;
            }

            //Better jump
            if (!Mover.GroundChecker.IsGrounded)
            {
                if (jumpPressed)
                {
                    Mover.GravityMultiplier = Mover.Velocity.y > 0 ? risingPressedMultiplier : fallingPressedMultiplier;
                }
                else
                {
                    Mover.GravityMultiplier = Mover.Velocity.y > 0 ? risingMultiplier : fallingMultiplier;
                }
            }
            else Mover.GravityMultiplier = 1;
        }
    
        private void FixedUpdate()
        {
            if (_jumpQueued && Mover.CanJump)
            {
                Mover.Jump();
                _jumpQueued = false;
                jumpCooldownTimer.Reset();
            }
            if (HorizontalMovement != 0) Mover.SetHorizontalMovement(HorizontalMovement);
        }
    }
}
