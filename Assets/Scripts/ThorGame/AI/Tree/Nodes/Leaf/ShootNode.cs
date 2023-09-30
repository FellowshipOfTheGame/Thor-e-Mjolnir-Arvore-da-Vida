using ThorGame.AI.Enemy;
using UnityEngine;

namespace ThorGame.AI.Tree.Nodes.Leaf
{
    public class ShootNode : TargetedNode
    {
        private EnemyRangedWeapon _ranged;
        private Collider2D _targetCol;
        private bool _couldShoot;
        protected override void Init(AIData data)
        {
            base.Init(data);
            _ranged = data.Runner.GetComponent<EnemyRangedWeapon>();
            _targetCol = Target.GetComponent<Collider2D>();
            
            var target = _targetCol.bounds.center;
            _couldShoot = _ranged.CanShoot(target);
            if (_couldShoot) _ranged.Shoot(target);
        }

        protected override NodeResult Process(AIData data)
        {
            if (!_couldShoot) return NodeResult.Failure;
            return _ranged.IsShooting ? NodeResult.Running : NodeResult.Success;
        }
    }
}