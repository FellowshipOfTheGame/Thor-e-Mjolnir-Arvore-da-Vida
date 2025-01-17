﻿using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace ThorGame
{
    public class EntityHealth: MonoBehaviour, IHealthProvider, IHittable
    {
        public event IHealthProvider.HealthUpdateEvent OnHealthChanged;
        
        [SerializeField] private int _maxHealth;
        public int MaxHealth => _maxHealth;

        [SerializeField] private bool destroyOnDeath;
        
        public UnityEvent stunStart, stunEnd, deathEvent;

        private int _health;

        public int Health
        {
            get => _health;
            set
            {
                if (value == _health) return;
                
                int oldHealth = _health;
                _health = Mathf.Clamp(value, 0, _maxHealth);

                if (Health <= 0) Die();
                OnHealthChanged?.Invoke(oldHealth, _health);
            }
        }
        
        public bool IsStunned { get; private set; }

        private void Awake()
        {
            _health = _maxHealth;
        }
        
        public void Heal(int healAmount)
        {
            Health += healAmount;
        }

        public void Damage(int dmg, float stunSeconds = 0)
        {
            Health -= dmg;
            if (stunSeconds > 0 && Health > 0) StartCoroutine(StunCoroutine(stunSeconds));
        }

        public void Die()
        {
            if (_health > 0) _health = 0;
            StopAllCoroutines();
            deathEvent?.Invoke();
            if (destroyOnDeath) Destroy(gameObject);
        }

        public void Hit(Vector2 point, Vector2 velocity, int damage)
        {
            if (Health == 0) return;
            Damage(damage);
        }


        private IEnumerator StunCoroutine(float seconds)
        {
            Timer timer = new(seconds);
            IsStunned = true;
            stunStart?.Invoke();
            yield return new WaitUntil(() => timer.Tick());
            IsStunned = false;

            if (Health == 0) yield break;
            stunEnd?.Invoke();
        }
    }
}