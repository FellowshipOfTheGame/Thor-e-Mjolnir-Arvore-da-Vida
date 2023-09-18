using System.Collections;
using UnityEngine;

namespace ThorGame.AI.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class AnimatedRangedWeapon : EnemyRangedWeapon
    {
        [SerializeField] private string shotAnimationName;
        
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
            _animator.Play(shotAnimationName);
            yield return new WaitUntil(() => _animationCallback);
            _animationCallback = false;
            LaunchProjectile(target);
        }
        
        protected override void DoShoot(Vector3 target)
        {
            StartCoroutine(ShootAnimationCoroutine(target));
        }
    }
}