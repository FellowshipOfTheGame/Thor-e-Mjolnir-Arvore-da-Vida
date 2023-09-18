
using UnityEngine;

namespace ThorGame
{
    public class EntityHealth: MonoBehaviour, IHealthProvider, IHittable
    {
        public event IHealthProvider.HealthUpdateEvent OnHealthChanged;

        [SerializeField] private int _maxHealth;
        public int MaxHealth => _maxHealth;

        private int _health;

        public int Health
        {
            get => _health;
            set
            {
                int oldHealth = _health;
                _health = Mathf.Clamp(value, 0, _maxHealth);

                if (Health <= 0) Die();
                OnHealthChanged?.Invoke(oldHealth, _health);
            }
        }

        private void Awake()
        {
            _health = _maxHealth;
        }

        public void Damage(int dmg)
        {
            Health -= dmg;
        }

        public void Die()
        {
            if (_health > 0) _health = 0;
            Destroy(gameObject);
        }

        public void Hit(Vector2 point, Vector2 velocity)
        {
            Damage(1);
        }
    }
}