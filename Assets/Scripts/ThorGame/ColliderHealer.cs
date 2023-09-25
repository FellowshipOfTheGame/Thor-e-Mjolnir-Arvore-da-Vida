using UnityEngine;

namespace ThorGame
{
    public class ColliderHealer : MonoBehaviour
    {
        [SerializeField] private int healAmount = 1;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<EntityHealth>(out var health) && health.Health > 0 && health.Health < health.MaxHealth)
            {
                health.Heal(healAmount);
                Destroy(gameObject);
            }
        }
    }
}