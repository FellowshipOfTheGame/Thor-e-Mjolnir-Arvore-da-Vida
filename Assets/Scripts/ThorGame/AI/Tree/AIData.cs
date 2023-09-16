using UnityEngine;

namespace ThorGame.AI.Tree
{
    [System.Serializable]
    public class AIData
    {
        public EnemyAIRunner Runner { get; set; }
        [field: SerializeField]
        public Transform[] Targets { get; set; }
    }
}