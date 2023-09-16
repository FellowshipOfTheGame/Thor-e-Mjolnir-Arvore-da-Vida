using ThorGame.AI.Enemy;
using UnityEngine;

namespace ThorGame.AI.Tree.Nodes.Leaf
{
    public class InRangedRangeNode : LeafNode
    {
        [SerializeField] private int targetIndex;
        
        private EnemyRangedWeapon _ranged;
        protected override void Init(AIData data)
        {
            _ranged = data.Runner.GetComponent<EnemyRangedWeapon>();
        }

        protected override NodeResult Process(AIData data)
        {
            var target = data.Targets[targetIndex].position;
            return _ranged.IsInRange(target) ? NodeResult.Success : NodeResult.Failure;
        }
    }
}