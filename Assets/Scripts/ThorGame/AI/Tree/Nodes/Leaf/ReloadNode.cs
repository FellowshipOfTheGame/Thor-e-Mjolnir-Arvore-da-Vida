using ThorGame.AI.Enemy;

namespace ThorGame.AI.Tree.Nodes.Leaf
{
    public class ReloadNode : LeafNode
    {
        private EnemyRangedWeapon _ranged;
        protected override void Init(AIData data)
        {
            _ranged = data.Runner.GetComponent<EnemyRangedWeapon>();
            _ranged.Ammo.StartReload();
        }

        protected override NodeResult Process(AIData data)
        {
            return _ranged.Ammo.HasAmmoReady ? NodeResult.Success : NodeResult.Running;
        }
    }
}