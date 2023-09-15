using System.Collections.Generic;

namespace ThorGame.AI.Tree.Nodes.Composite
{
    public class SequenceNode : CompositeNode
    {
        protected override NodeResult Process(IEnumerable<AINode> children)
        {
            //Todo running
            foreach (var node in children)
            {
                var res = node.Tick();
                if (res == NodeResult.Failure) return NodeResult.Failure;
            }
            return NodeResult.Success;
        }
    }
}