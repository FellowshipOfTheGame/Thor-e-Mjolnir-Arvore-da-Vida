using UnityEngine;

namespace ThorGame.Trees
{
    public abstract class TypedConnection<TNode, TConnection> : ScriptableObject, ICloneable<TConnection>
        where TNode : TypedNode<TNode, TConnection>
        where TConnection : TypedConnection<TNode, TConnection>
    {
        [HideInInspector] [SerializeField] protected TNode from, to;

        public TNode From => from;
        public TNode To => to;


        public virtual TConnection Clone()
        {
            var clone = (TConnection)Instantiate(this);
            clone.from = from;
            clone.to = to;
            return clone;
        }
    }
}