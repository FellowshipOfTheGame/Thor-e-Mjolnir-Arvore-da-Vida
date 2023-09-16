using ThorGame.Trees;
using UnityEngine;

namespace ThorGame.AI.Tree.Nodes.Leaf
{
    public abstract class LeafNode: AINode
    {
        public override ConnectionCount OutputConnection => ConnectionCount.None;

        protected abstract void Init(AIData data);
        protected abstract NodeResult Process(AIData data);

        private bool _running;
        public override NodeResult Tick(AIData data)
        {
            if (!_running) Init(data);
            
            var res = Process(data);
            _running = res == NodeResult.Running;
            Debug.Log(this.GetType().Name + ": " + res);
            return res;
        }
    }
}