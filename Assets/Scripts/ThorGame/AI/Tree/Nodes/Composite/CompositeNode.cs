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

        protected abstract NodeResult Process();
        public override NodeResult Tick()
        {
            var result = Process();
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