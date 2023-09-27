using UnityEngine;

namespace ThorGame.AI.Enemy
{
    [RequireComponent(typeof(Projectile), typeof(Rigidbody2D))]
    public class RedirectableProjectile : MonoBehaviour, IHittable
    {
        [SerializeField] private float bounce;
        [SerializeField] [Range(0,1)] private float hammerVelBounceWeight = 1f;
        private Rigidbody2D _rb;
        private Projectile _projectile;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _projectile = GetComponent<Projectile>();
        }

        public void Hit(Vector2 point, Vector2 velocity, int damage)
        {
            _projectile.enabled = false;
            _rb.velocity = Vector2.zero;
            _rb.AddForce(-bounce * _projectile.Direction + (velocity * hammerVelBounceWeight), ForceMode2D.Impulse);
        }

        bool IHittable.RequireMinSpeed => false;
    }
}