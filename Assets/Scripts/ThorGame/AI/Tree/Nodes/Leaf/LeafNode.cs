using ThorGame.Trees;

namespace ThorGame.AI.Tree.Nodes.Leaf
{
    public abstract class LeafNode : AINode
    {
        public override ConnectionCount OutputConnection => ConnectionCount.None;

        protected abstract void Init(AIData data);
        protected abstract NodeResult Process(AIData data);
        
        protected bool InitThisFrame { get; private set; }

        private bool _running;
        public override NodeResult Tick(AIData data)
        {
            if (!_running)
            {
                Init(data);
                InitThisFrame = true;
            }
            
            var res = Process(data);
            _running = res == NodeResult.Running;
            InitThisFrame = false;
            return res;
        }
    }
}