using UnityEngine;

namespace ThorGame.Player
{
    [RequireComponent(typeof(Animator), typeof(PlayerMover), typeof(ThorController))]
    public class PlayerAnimator : MonoBehaviour
    {
        [Header("Animations")] 
        [SerializeField] private NamedHash idleAnimation = "Idle";
        [SerializeField] private NamedHash runAnimation = "Run";
        [SerializeField] private NamedHash groundedBlendTree = "GroundedBlendTree";
        [Header("Parameters")]
        [SerializeField] private NamedHash speedParam = "Speed";
        
        private Animator _animator;
        private PlayerMover _mover;
        private ThorController _controller;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _mover = GetComponent<PlayerMover>();
            _controller = GetComponent<ThorController>();
        }

        private NamedHash _currentAnimation;
        private NamedHash GetTargetAnimation()
        {
            return groundedBlendTree;
            /*if (_mover.GroundChecker.IsGrounded)
            {
                return _mover.Velocity.x != 0 ? runAnimation : idleAnimation;
            }
            return idleAnimation;*/
        }

        private void SetAnimation(NamedHash target)
        {
            _currentAnimation = target;
            _animator.Play(target.Hash);
        }

        private void UpdateParameters()
        {
            _animator.SetFloat(speedParam.Hash, Mathf.Abs(_controller.HorizontalMovement));
        }

        private void Update()
        {
            UpdateParameters();
            NamedHash target = GetTargetAnimation();
            if (target != _currentAnimation)
            {
                SetAnimation(target);
            }
        }
    }
}