
namespace ThorGame.AI.Tree.Nodes.Composite
{
    public class SequenceNode : CompositeNode
    {
        protected override NodeResult Process()
        {
            for (int i = RunningNodeIndex; i < Children.Length; i++)
            {
                switch (Children[i].Tick())
                {
                    case NodeResult.Failure:
                        return NodeResult.Failure;
                    case NodeResult.Running:
                        RegisterRunning(i);
                        return NodeResult.Running;
                }
            }
            return NodeResult.Success;
        }
    }
}