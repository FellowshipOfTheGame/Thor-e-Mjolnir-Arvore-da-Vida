
namespace ThorGame.AI.Tree.Nodes.Composite
{
    public class SelectorNode : CompositeNode
    {
        protected  override NodeResult Process()
        {
            for (int i = RunningNodeIndex; i < Children.Length; i++)
            {
                switch (Children[i].Tick())
                {
                    case NodeResult.Success:
                        return NodeResult.Success;
                    case NodeResult.Running:
                        RegisterRunning(i);
                        return NodeResult.Running;
                }
            }
            return NodeResult.Failure;
        }
    }
}