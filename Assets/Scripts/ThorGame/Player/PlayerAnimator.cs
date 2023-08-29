using System;
using UnityEngine;

namespace ThorGame.Player
{
    [RequireComponent(typeof(Animator), typeof(PlayerMover), typeof(ThorController))]
    public class PlayerAnimator : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private NamedHash speed = "Speed";
        [SerializeField] private NamedHash grounded = "Grounded";
        [SerializeField] private NamedHash jump = "Jump";
        [SerializeField] private NamedHash dead = "Dead";
        
        private Animator _animator;
        private PlayerMover _mover;
        private ThorController _controller;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _mover = GetComponent<PlayerMover>();
            _controller = GetComponent<ThorController>();
        }

        private void TriggerJump() => _animator.SetTrigger(jump.Hash);
        private void OnEnable()
        {
            _mover.onJump += TriggerJump;
        }
        private void OnDisable()
        {
            _mover.onJump -= TriggerJump;
        }

        private void Update()
        {
            _animator.SetFloat(speed.Hash, Mathf.Abs(_controller.HorizontalMovement));
            _animator.SetBool(grounded.Hash, _mover.GroundChecker.IsGrounded);
        }
    }
}