using ThorGame.Trees;

namespace ThorGame.AI.Tree.Nodes
{
    public abstract class AINode : TypedNode<AINode, AIConnection>
    {
        public abstract NodeResult Tick();
        
        public override ConnectionCount InputConnection => ConnectionCount.Single;
    }
}