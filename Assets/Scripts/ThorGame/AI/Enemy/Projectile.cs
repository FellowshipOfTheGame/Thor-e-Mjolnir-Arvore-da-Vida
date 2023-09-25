using System;
using UnityEngine;

namespace ThorGame.AI.Enemy
{
    [RequireComponent(typeof(RigidBody2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private float speed;

        private Vector3 _direction;
        Private RigidBody2D _rb;
        private void Awake()
        {
            _rb = GetComponent<RigidBody2D>();
        }
        
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
                Destroy(gameObject);
            }
        }
        
        ivate void SetDirection(

        public static Projectile Shoot(Projectile prefab, Vector3 origin, Vector3 direction)
        {
            var instance = Instantiate(prefab, origin, Quaternion.identity);
            instance._direction = direction.normalized;
            return instance;
        }
    }
}