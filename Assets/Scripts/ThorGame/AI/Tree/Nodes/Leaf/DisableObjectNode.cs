using UnityEngine;

namespace ThorGame.AI.Tree.Nodes.Leaf
{
    public class DisableObjectNode : TargetedNode
    {
        [SerializeField] private bool active;

        protected override NodeResult Process(AIData data)
        {
            Target.gameObject.SetActive(active);
            return NodeResult.Success;
        }
    }
}