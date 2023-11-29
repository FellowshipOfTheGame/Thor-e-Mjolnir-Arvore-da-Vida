using System.Collections.Generic;
using System.Linq;
using ThorGame.AI.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ThorGame.Player
{
    public class VoiceLineController : MonoBehaviour
    {
        [System.Serializable]
        private struct EnemyLinePair
        {
            public string enemyTag;
            public AudioClip[] lines;
        }
        
        [SerializeField] private float radius;
        [SerializeField] private LayerMask mask;
        [SerializeField] private Timer cooldown;
        [Range(0f, 1f)] [SerializeField] private float chance;
        [SerializeField] private EnemyLinePair[] entries;

        private AudioSource _source;
        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            cooldown.Complete();
        }

        private void Update()
        {
            if (_source.isPlaying || !cooldown.Tick()) return;

            var enemy = Physics2D.OverlapCircle(transform.position, radius, mask);
            if (!enemy) return;
            
            var pair = entries.FirstOrDefault(e => enemy.gameObject.CompareTag(e.enemyTag));
            if (pair.lines == null || pair.lines.Length == 0) return;
            var line = pair.lines[Random.Range(0, pair.lines.Length)];
            
            if (Random.value > chance)
            {
                cooldown.Reset();
                return;
            }
            _source.PlayOneShot(line);
            cooldown.Reset();
        }
    }
}
