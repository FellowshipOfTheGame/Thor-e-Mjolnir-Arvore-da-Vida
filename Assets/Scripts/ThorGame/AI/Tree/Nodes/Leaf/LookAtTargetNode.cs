using ThorGame.Player;
using UnityEngine;

namespace ThorGame.AI.Tree.Nodes.Leaf
{
    public class LookAtTargetNode : TargetedNode
    {
        private PlayerMover _mover;
        protected override void Init(AIData data)
        {
            base.Init(data);
            _mover = data.Runner.GetComponent<PlayerMover>();
        }

        protected override NodeResult Process(AIData data)
        {
            Vector2 off = Target.position - data.Runner.transform.position;
            _mover.SetDirection(off.x);
            return NodeResult.Success;
        }
    }
}