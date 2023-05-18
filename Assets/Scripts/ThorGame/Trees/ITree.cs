using System.Collections.Generic;

namespace ThorGame.Trees
{
    public interface ITree
    {
        INode Root { get; }
        IEnumerable<INode> AllNodes { get; }

        void Tick(object data);
    }
    
    public interface ITypedTree<out TNode, in TData, out TReturn> : ITree 
        where TNode: INode
    {
        new TNode Root { get; }
        INode ITree.Root => Root;
        
        new IEnumerable<TNode> AllNodes { get; }
        IEnumerable<INode> ITree.AllNodes => (IEnumerable<INode>)AllNodes;

        void Tick(TData data);
        void ITree.Tick(object data) => Tick((TData)data);
    }
}