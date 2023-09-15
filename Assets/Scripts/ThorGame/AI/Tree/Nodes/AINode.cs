using ThorGame.Trees;

namespace ThorGame.AI.Tree.Nodes
{
    public abstract class AINode : TypedNode<AINode, AIConnection>
    {
        public abstract NodeResult Tick(AIData data);
        
        public override ConnectionCount InputConnection => ConnectionCount.Single;
    }
}