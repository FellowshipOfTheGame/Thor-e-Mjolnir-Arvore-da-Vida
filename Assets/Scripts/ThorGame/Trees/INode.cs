using System;
using System.Collections.Generic;

namespace ThorGame.Trees
{
    public interface INode
    {
        IEnumerable<ITransition> TransitionsOut { get; }
        IEnumerable<ITransition> TransitionsIn { get; }

        void Begin(object data);
        object Tick(object data);
        void End(object data);
    }

    public interface ITypedNode<in TData, out TReturn, out TNode> : INode
        where TNode : ITypedNode<TData, TReturn, TNode>
    {
        new IEnumerable<ITypedTransition<TData, TReturn, TNode, TNode>> TransitionsOut { get; }
        IEnumerable<ITransition> INode.TransitionsOut => TransitionsOut;
        
        new IEnumerable<ITypedTransition<TData, TReturn, TNode, TNode>> TransitionsIn { get; }
        IEnumerable<ITransition> INode.TransitionsIn => TransitionsIn;
        
        void Begin(TData data);
        void INode.Begin(object data) => Begin((TData)data);
        
        TReturn Tick(TData data);
        object INode.Tick(object data) => Tick((TData)data);
        
        void End(TData data);
        void INode.End(object data) => End((TData)data);
    }
}