using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ThorGame
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private EntityHealth health;
        [SerializeField] private Image image;
        [SerializeField] private bool startInvisible;

        [SerializeField] private UnityEvent firstChangeEvent;
        
        private void Awake()
        {
            image.fillAmount = startInvisible ? 0 : 1;
            health.OnHealthChanged += HealthChanged;
        }

        private void HealthChanged(int oldHealth, int newHealth)
        {
            if (oldHealth == health.MaxHealth) firstChangeEvent.Invoke();
            image.fillAmount = (float)newHealth / health.MaxHealth;
        }
    }
}