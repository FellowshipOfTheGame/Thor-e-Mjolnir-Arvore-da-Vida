
namespace ThorGame.AI.Tree.Nodes.Composite
{
    public class SelectorNode : CompositeNode
    {
        protected override NodeResult Process(AIData data)
        {
            for (int i = RunningNodeIndex; i < Children.Length; i++)
            {
                switch (Children[i].Tick(data))
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