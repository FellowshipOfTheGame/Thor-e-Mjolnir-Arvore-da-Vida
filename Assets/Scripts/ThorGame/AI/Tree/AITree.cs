using ThorGame.AI.Tree.Nodes;
using ThorGame.Trees;
using UnityEngine;

namespace ThorGame.AI.Tree
{
    [CreateAssetMenu(fileName = "AITree", menuName = "AI/Tree", order = 0)]
    public class AITree : TypedTree<AITree, AINode, AIConnection>
    {
        public void Tick()
        {
            root.Tick();
        }
    }
}