using System;
using ThorGame.Trees;
using UnityEngine;

namespace ThorGame.AI.Tree.Nodes
{
    public abstract class AINode : TypedNode<AINode, AIConnection>
    {
        public abstract NodeResult Tick(AIData data);
        
        public override ConnectionCount InputConnection => ConnectionCount.Single;
    }
}