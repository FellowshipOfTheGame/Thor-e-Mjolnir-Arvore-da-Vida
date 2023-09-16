using ThorGame.Player;
using UnityEngine;

namespace ThorGame.AI.Tree
{
    public class EnemyAIRunner : MonoBehaviour
    {
        [SerializeField] private AITree tree;
        [SerializeField] private AIData data;

        [SerializeField] private bool setPlayerAsTarget0;

        private void Awake()
        {
            tree = tree.Clone();
            data.Runner = this;
        }

        private void Start()
        {
            if (setPlayerAsTarget0)
            {
                if (data.Targets == null || data.Targets.Length == 0)
                {
                    data.Targets = new Transform[1];
                }

                data.Targets[0] = FindObjectOfType<ThorController>().transform;
            }
        }

        private void Update()
        {
            tree.Tick(data);
        }
    }
}