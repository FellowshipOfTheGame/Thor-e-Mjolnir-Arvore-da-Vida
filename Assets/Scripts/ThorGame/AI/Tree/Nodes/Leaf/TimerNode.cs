using UnityEngine;

namespace ThorGame.AI.Tree.Nodes.Leaf
{
    public class TimerNode : LeafNode
    {
        [SerializeField] private Timer timer;

        protected override void Init(AIData data)
        {
            timer.Reset();
        }

        protected override NodeResult Process(AIData data)
        {
            return timer.Tick() ? NodeResult.Success : NodeResult.Running;
        }
    }
}