﻿using System.Collections.Generic;

namespace ThorGame.AI.Tree.Nodes.Composite
{
    public class SelectorNode : CompositeNode
    {
        protected override NodeResult Process(IEnumerable<AINode> children)
        {
            //Todo running
            foreach (var node in children)
            {
                var res = node.Tick();
                if (res == NodeResult.Success) return NodeResult.Success;
            }
            return NodeResult.Failure;
        }
    }
}