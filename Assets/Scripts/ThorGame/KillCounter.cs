using System.Collections.Generic;
using ThorGame.Player;
using UnityEngine;

namespace ThorGame
{
    public class KillCounter : MonoBehaviour
    {
        private HashSet<EntityHealth> _enemies = new();
        private void Start()
        {
            foreach (var enemy in FindObjectsOfType<EntityHealth>(true))
            {
                if (enemy.GetComponent<ThorController>()) continue;
                _enemies.Add(enemy);
                enemy.deathEvent.AddListener(() => OnEnemyDead(enemy));
            }
        }

        private void OnEnemyDead(EntityHealth enemy)
        {
            _enemies.Remove(enemy);
            if (_enemies.Count == 0)
            {
                GameLoader.Instance.LoadVictory(true);
            }
        }
    }
}