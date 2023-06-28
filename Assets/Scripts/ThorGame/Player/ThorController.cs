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

        public PlayerMover Mover { get; private set; }
        private void Awake()
        {
            Mover = GetComponent<PlayerMover>();
        }

        private float _horizontalMovement;
        private bool _jumpQueued;
        private void Update()
        {
            _horizontalMovement = Input.GetAxis(moveAxis);

            if (jumpCooldownTimer.Tick() && Input.GetKey(jumpKey))
            {
                _jumpQueued = true;
                jumpQueueTimer.Reset();
            }
            else if (_jumpQueued && jumpQueueTimer.Tick())
            {
                _jumpQueued = false;
            }
            
            //DEBUG
            //Debug.DrawRay(transform.position, Vector3.right * _horizontalMovement, Color.yellow);
        }
    
        private void FixedUpdate()
        {
            if (_jumpQueued && Mover.CanJump)
            {
                Mover.Jump();
                _jumpQueued = false;
                jumpCooldownTimer.Reset();
            }
            if (_horizontalMovement != 0) Mover.MoveHorizontally(_horizontalMovement);
        }
    }
}
