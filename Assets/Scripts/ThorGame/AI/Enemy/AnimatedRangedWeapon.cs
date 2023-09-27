using System.Collections;
using UnityEngine;

namespace ThorGame.AI.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class AnimatedRangedWeapon : EnemyRangedWeapon
    {
        [SerializeField] private string shotAnimationName;

        private bool _isShooting;
        public override bool IsShooting => _isShooting;

        private Animator _animator;
        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
        }

        private bool _animationCallback;
        public void AnimationCallback() => _animationCallback = true;
        
        private IEnumerator ShootAnimationCoroutine(Vector3 target)
        {
            _isShooting = true;
            _animator.Play(shotAnimationName);
            yield return new WaitUntil(() => _animationCallback);
            _animationCallback = false;
            _isShooting = false;
            LaunchProjectile(target);
        }
        
        protected override void DoShoot(Vector3 target)
        {
            StartCoroutine(ShootAnimationCoroutine(target));
        }
    }
}