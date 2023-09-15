using System.Linq;
using ThorGame.Trees;

namespace ThorGame.AI.Tree.Nodes.Composite
{
    public abstract class CompositeNode : AINode
    {
        public override ConnectionCount OutputConnection => ConnectionCount.Multi;

        private AINode[] _children;
        protected AINode[] Children
            => _children ??= connections.Select(c => c.To).ToArray();

        protected abstract NodeResult Process(AIData data);
        public override NodeResult Tick(AIData data)
        {
            var result = Process(data);
            if (result != NodeResult.Running) RunningNodeIndex = 0;
            return result;
        }

        protected int RunningNodeIndex { get; private set; }
        protected void RegisterRunning(int i)
        {
            RunningNodeIndex = i;
        }
    }
}