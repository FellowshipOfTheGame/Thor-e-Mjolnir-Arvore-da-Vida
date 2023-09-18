using UnityEngine;

namespace ThorGame.Trees
{
    public abstract class TypedConnection<TNode, TConnection> : ScriptableObject, IConnection
        where TNode : TypedNode<TNode, TConnection>
        where TConnection : TypedConnection<TNode, TConnection>
    {
        [HideInInspector] [SerializeField] private TNode from, to;

        public TNode From => from;
        public TNode To => to;

        INode IConnection.From => From;
        INode IConnection.To => To;


        public virtual TConnection Clone(TNode fromClone, TNode toClone)
        {
            var clone = (TConnection)Instantiate(this);
            clone.from = fromClone;
            clone.to = toClone;
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