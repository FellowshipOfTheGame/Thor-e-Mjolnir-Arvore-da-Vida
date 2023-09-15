using System.Collections.Generic;
using System.Linq;
using ThorGame.Trees;

namespace ThorGame.AI.Tree.Nodes.Composite
{
    public abstract class CompositeNode : AINode
    {
        public override ConnectionCount OutputConnection => ConnectionCount.Multi;

        public override NodeResult Tick()
        {
            return Process(connections.Select(c => c.To));
        }

        protected abstract NodeResult Process(IEnumerable<AINode> children);
    }
}