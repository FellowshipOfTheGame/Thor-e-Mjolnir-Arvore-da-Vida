using System;
using System.Linq;
using ThorGame.Trees;

namespace ThorGame.AI.Tree.Nodes.Decorator
{
    public abstract class DecoratorNode: AINode
    {
        public override ConnectionCount OutputConnection => ConnectionCount.Single;

        protected AINode Child
        {
            get
            {
                if (connections.Count == 0) throw new Exception("No child for decorator node");
                return connections[0].To;
            }
        }

        public override NodeResult Tick()
        {
            return Decorate(Child.Tick());
        }

        protected abstract NodeResult Decorate(NodeResult result);
    }
}