using UnityEngine;

namespace ThorGame.AI.Enemy
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private float speed;

        private Vector3 _direction;
        private void Update()
        {
            transform.position += _direction * (speed * Time.deltaTime);
            transform.up = _direction;
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent<EntityHealth>(out var health))
            {
                health.Damage(damage);
            }
        }

        public static Projectile Shoot(Projectile prefab, Vector3 origin, Vector3 direction)
        {
            var instance = Instantiate(prefab, origin, Quaternion.identity);
            instance._direction = direction.normalized;
            return instance;
        }
    }
}