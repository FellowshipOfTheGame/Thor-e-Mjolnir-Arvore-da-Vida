using ThorGame.AI.Enemy;
using UnityEngine;

namespace ThorGame.AI.Tree.Nodes.Leaf
{
    public class ShootNode : LeafNode
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
            if (!_ranged.CanShoot(target)) return NodeResult.Failure;
            _ranged.Shoot(target);
            return NodeResult.Success;
        }
    }
}