using UnityEngine;

namespace ThorGame.AI.Tree
{
    public class EnemyAIRunner : MonoBehaviour
    {
        [SerializeField] private AITree tree;
        [SerializeField] private AIData data;

        private void Awake()
        {
            tree = tree.Clone();
            data.Runner = this;
        }

        private void Update()
        {
            tree.Tick(data);
        }
    }
}