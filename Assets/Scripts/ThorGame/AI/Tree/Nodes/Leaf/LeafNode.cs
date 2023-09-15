using ThorGame.Trees;

namespace ThorGame.AI.Tree.Nodes.Leaf
{
    public abstract class LeafNode: AINode
    {
        public override ConnectionCount OutputConnection => ConnectionCount.None;
    }
}