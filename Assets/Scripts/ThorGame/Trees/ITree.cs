using System.Collections.Generic;

namespace ThorGame.Trees
{
    public interface ITree<in TData, out TReturn>
    {
        INode<TData, TReturn> Root { get; }
        IEnumerable<INode<TData, TReturn>> AllNodes { get; }

        void Tick(TData data) => Root.Tick(data);
    }
    
    public interface ITypedTree<out TNode, in TData, out TReturn> : ITree<TData, TReturn> 
        where TNode: INode<TData, TReturn>
    {
        new TNode Root { get; }
        INode<TData, TReturn> ITree<TData, TReturn>.Root => Root;
        
        new IEnumerable<TNode> AllNodes { get; }
        IEnumerable<INode<TData, TReturn>> ITree<TData, TReturn>.AllNodes => (IEnumerable<INode<TData, TReturn>>)AllNodes;
    }
}