using UnityEngine;

namespace ThorGame.AI.Tree.Nodes.Leaf
{
    public abstract class TargetedNode : LeafNode
    {
        [SerializeField] private int targetIndex;

        protected int TargetIndex => targetIndex;
        protected Transform Target { get; private set; }
        protected override void Init(AIData data)
        {
            Target = data.Targets[targetIndex];
        }
    }
}