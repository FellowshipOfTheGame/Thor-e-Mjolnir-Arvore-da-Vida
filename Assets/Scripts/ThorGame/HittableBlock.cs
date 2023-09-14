using UnityEngine;
using UnityEngine.Events;

namespace ThorGame
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class HittableBlock : MonoBehaviour, IHittable, IHealthProvider
    {
        [field: SerializeField]
        public int MaxHealth { get; private set; }
        
        [SerializeField] private float breakHitMinSpeed;
        public UnityEvent breakEvent;
        
        public int Health { get; private set; }
        public event IHealthProvider.HealthUpdateEvent OnHealthChanged;

        private int _hitCount;
        private Rigidbody2D _rb;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            Health = MaxHealth;
        }

        public void Hit(Vector2 point, Vector2 velocity)
        {
            if (velocity.sqrMagnitude < breakHitMinSpeed * breakHitMinSpeed) return;
            
            _rb.AddForceAtPosition(velocity, point);
            Health--;
            OnHealthChanged?.Invoke(Health + 1, Health);
            if (Health <= 0) Break();
        }

        public void Break()
        {
            breakEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}