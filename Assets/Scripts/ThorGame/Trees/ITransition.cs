namespace ThorGame.Trees
{
    public interface ITransition<in TData, out TReturn>
    {
        INode<TData, TReturn> From { get; }
        INode<TData, TReturn> To { get; }

        bool Condition(TData data);
    }

    public interface ITypedTransition<in TData, out TReturn, out TFrom, out TTo> : ITransition<TData, TReturn> 
        where TFrom: INode<TData, TReturn>
        where TTo: INode<TData, TReturn>
    {
        new TFrom From { get; }
        INode<TData, TReturn> ITransition<TData, TReturn>.From => From;
        
        new TTo To { get; }
        INode<TData, TReturn> ITransition<TData, TReturn>.To => To;
    }
}