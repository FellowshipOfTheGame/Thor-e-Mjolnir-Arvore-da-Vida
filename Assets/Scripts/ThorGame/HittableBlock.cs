using UnityEngine;

namespace ThorGame
{
    /*[RequireComponent(typeof(Rigidbody2D))]
    public class HittableBlock : MonoBehaviour, IHittable
    {
        [SerializeField] private int breakHitCount = 5;

        private int _hitCount;
        private Rigidbody2D _rb;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Hit(Vector2 point, Vector2 velocity)
        {
            _rb.AddForceAtPosition(velocity, point);
            _hitCount++;
            if (_hitCount >= breakHitCount) Destroy(gameObject);
        }
    }*/
}