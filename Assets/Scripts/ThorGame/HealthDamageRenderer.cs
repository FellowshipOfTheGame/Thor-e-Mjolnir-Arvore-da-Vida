using UnityEngine;

namespace ThorGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class HealthDamageRenderer : MonoBehaviour
    {
        [SerializeField] private Gradient zeroToFullHealthGradient;

        private SpriteRenderer _renderer;
        private IHealthProvider _health;
        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _health = GetComponent<IHealthProvider>();
        }

        private void OnEnable() => _health.OnHealthChanged += OnHealthChanged;
        private void OnDisable() => _health.OnHealthChanged -= OnHealthChanged;

        private void OnHealthChanged(int oldHealth, int newHealth)
        {
            float progress = (float)newHealth / _health.MaxHealth;
            _renderer.color = zeroToFullHealthGradient.Evaluate(progress);
        }
    }
}