using System;
using ThorGame.Player;
using UnityEngine;

namespace ThorGame
{
    public class Damager : MonoBehaviour
    {
        [SerializeField] private int damage = 1;
        [SerializeField] private float knockBack;
        [SerializeField] private float stun;
        [SerializeField] private Timer cooldown;
        
        private void Awake()
        {
            cooldown.Complete();
        }

        private void Update()
        {
            cooldown.Tick();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!cooldown.Completed) return;
            
            if (col.TryGetComponent<EntityHealth>(out var health))
            {
                health.Damage(damage, stun);
                
                cooldown.Reset();
            }

            if (knockBack > 0 && col.TryGetComponent<PlayerMover>(out var mover))
            {
                float dir = Mathf.Sign(col.transform.position.x - transform.position.x);
                mover.KnockBack(dir, knockBack);
            }
        }
    }
}