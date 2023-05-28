using UnityEngine;

namespace ThorGame.Trees
{
    public abstract class TypedConnection<TNode, TConnection> : ScriptableObject, IConnection, ICloneable<TConnection>
        where TNode : TypedNode<TNode, TConnection>
        where TConnection : TypedConnection<TNode, TConnection>
    {
        [HideInInspector] [SerializeField] protected TNode from, to;

        public TNode From => from;
        public TNode To => to;

        INode IConnection.From => From;
        INode IConnection.To => To;


        public virtual TConnection Clone()
        {
            var clone = (TConnection)Instantiate(this);
            clone.from = from;
            clone.to = to;
            return clone;
        }
        
#if UNITY_EDITOR
        [HideInInspector] [SerializeField] public string treeGuid;
        
        string IConnection.TreeTitle => name;

        string IConnection.TreeGuid
        {
            get => treeGuid;
            set => treeGuid = value;
        }
#endif
    }
}