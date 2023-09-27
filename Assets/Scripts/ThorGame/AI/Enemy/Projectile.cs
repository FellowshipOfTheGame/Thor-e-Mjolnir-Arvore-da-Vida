using System.Collections.Generic;
using UnityEngine;

namespace ThorGame.AI.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private float speed;

        private Rigidbody2D _rb;
        private Collider2D _col;
        public Vector2 Direction { get; private set; }

        private void Update()
        {
            transform.position += (Vector3)Direction * (speed * Time.deltaTime);
            transform.up = Direction;
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!enabled) return;
            if (col.TryGetComponent<EntityHealth>(out var health))
            {
                health.Damage(damage);
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            if (!_rb)
            {
                _rb = GetComponent<Rigidbody2D>();
                _col = GetComponent<Collider2D>();
            }
            _rb.isKinematic = true;
            _col.isTrigger = true;
        }

        private void OnDisable()
        {
            _rb.isKinematic = false;
            _col.isTrigger = false;
        }

        public static Projectile Shoot(Projectile prefab, Vector3 origin, Vector3 direction)
        {
            var instance = Instantiate(prefab, origin, Quaternion.identity);
            instance.Direction = direction.normalized;
            return instance;
        }
    }
}