using System.Collections.Generic;

namespace ThorGame.Trees
{
    public interface ITree
    {
        IEnumerable<INode> AllNodes { get; }
    }
}