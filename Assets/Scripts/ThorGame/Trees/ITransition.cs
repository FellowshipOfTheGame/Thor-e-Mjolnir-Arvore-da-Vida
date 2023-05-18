namespace ThorGame.Trees
{
    public interface ITransition
    {
        INode From { get; }
        INode To { get; }
    }

    public interface ITypedTransition<in TData, out TReturn, out TFrom, out TTo> : ITransition 
        where TFrom: ITypedNode<TData, TReturn, TFrom>
        where TTo: ITypedNode<TData, TReturn, TTo>
    {
        new TFrom From { get; }
        INode ITransition.From => From;
        
        new TTo To { get; }
        INode ITransition.To => To;
    }
}