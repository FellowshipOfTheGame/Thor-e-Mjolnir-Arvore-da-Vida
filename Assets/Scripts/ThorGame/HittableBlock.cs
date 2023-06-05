using UnityEngine;
using UnityEngine.Events;

namespace ThorGame
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class HittableBlock : MonoBehaviour, IHittable
    {
        [SerializeField] private float breakHitMinSpeed;
        [SerializeField] private int breakHitCount = 5;
        [SerializeField] private UnityEvent breakEvent;

        private int _hitCount;
        private Rigidbody2D _rb;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Hit(Vector2 point, Vector2 velocity)
        {
            if (velocity.sqrMagnitude < breakHitMinSpeed * breakHitMinSpeed) return;
            
            _rb.AddForceAtPosition(velocity, point);
            _hitCount++;
            if (_hitCount >= breakHitCount) Break();
        }

        public void Break()
        {
            breakEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}