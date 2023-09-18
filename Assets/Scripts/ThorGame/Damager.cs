using UnityEngine;

namespace ThorGame
{
    public class Damager : MonoBehaviour
    {
        [SerializeField] private int damage = 1;
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent<EntityHealth>(out var health))
            {
                health.Damage(damage);
            }
        }
    }
}