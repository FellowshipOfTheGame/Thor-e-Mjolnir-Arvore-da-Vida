using System;

namespace ThorGame.AI.Tree.Nodes.Decorator
{
    public class InverterNode : DecoratorNode
    {
        protected override NodeResult Decorate(NodeResult result)
        {
            return result switch
            {
                NodeResult.Success => NodeResult.Failure,
                NodeResult.Failure => NodeResult.Success,
                NodeResult.Running => NodeResult.Running,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}