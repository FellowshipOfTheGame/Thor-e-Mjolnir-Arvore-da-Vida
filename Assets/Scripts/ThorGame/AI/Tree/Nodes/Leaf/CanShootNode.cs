using ThorGame.AI.Enemy;

namespace ThorGame.AI.Tree.Nodes.Leaf
{
    public class CanShootNode : TargetedNode
    {
        private EnemyRangedWeapon _ranged;
        protected override void Init(AIData data)
        {
            base.Init(data);
            _ranged = data.Runner.GetComponent<EnemyRangedWeapon>();
        }

        protected override NodeResult Process(AIData data)
        {
            return _ranged.CanShoot(Target.position) ? NodeResult.Success : NodeResult.Failure;
        }
    }
}