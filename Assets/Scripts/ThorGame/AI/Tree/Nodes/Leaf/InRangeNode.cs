using System;
using UnityEngine;

namespace ThorGame.AI.Tree.Nodes.Leaf
{
    public class InRangeNode : TargetedNode
    {
        private enum Mode {Euclidean, Horizontal, Vertical}

        [SerializeField] private Mode mode = Mode.Euclidean;
        [SerializeField] private float range;
        protected override NodeResult Process(AIData data)
        {
            Vector3 off = (Target.position - data.Runner.transform.position);
            return mode switch
            {
                Mode.Euclidean => (off.sqrMagnitude <= range * range) ? NodeResult.Success : NodeResult.Failure,
                Mode.Horizontal => (Mathf.Abs(off.x) <= range) ? NodeResult.Success : NodeResult.Failure,
                Mode.Vertical => (Mathf.Abs(off.y) <= range) ? NodeResult.Success : NodeResult.Failure,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}