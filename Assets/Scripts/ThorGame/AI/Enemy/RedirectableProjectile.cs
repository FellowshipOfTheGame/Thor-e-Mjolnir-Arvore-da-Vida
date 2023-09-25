using UnityEngine;

namespace ThorGame.AI.Enemy
{
    [RequireComponent(typeof(Projectile), typeof(Rigidbody2D))]
    public class RedirectableProjectile : MonoBehaviour, IHittable
    {
        private Rigidbody2D _rb;
        private Projectile _projectile;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
            _projectile = GetComponent<Projectile>();
        }

        public void Hit(Vector2 point, Vector2 velocity, int damage)
        {
            _projectile.enabled = false;
            _rb.constraints = RigidbodyConstraints2D.None;
            _rb.velocity = Vector2.zero;
            _rb.AddForce(velocity, ForceMode2D.Impulse);
        }

        bool IHittable.RequireMinSpeed => false;
    }
}