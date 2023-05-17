using System;
using System.Collections.Generic;

namespace ThorGame.Trees
{
    public interface INode<in TData, out TReturn>
    {
        IEnumerable<ITransition<TData, TReturn>> Transitions { get; }

        void Begin(TData data);
        TReturn Tick(TData data);
        void End(TData data);
    }

    public interface ITypedNode<in TData, out TReturn, out TNode> : INode<TData, TReturn> 
        where TNode : ITypedNode<TData, TReturn, TNode>
    {
        new IEnumerable<ITypedTransition<TData, TReturn, TNode, TNode>> Transitions { get; }
        IEnumerable<ITransition<TData, TReturn>> INode<TData, TReturn>.Transitions => Transitions;
    }
}