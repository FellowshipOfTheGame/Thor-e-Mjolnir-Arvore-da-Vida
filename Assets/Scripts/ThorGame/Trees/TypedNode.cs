using System.Collections.Generic;
using UnityEngine;

namespace ThorGame.Trees
{
    public abstract class TypedNode<TNode, TConnection> : ScriptableObject, ICloneable<TNode>, INode
        where TNode : TypedNode<TNode, TConnection>
        where TConnection: TypedConnection<TNode, TConnection>
    {
        [HideInInspector] [SerializeField] protected bool isRoot;
        [HideInInspector] [SerializeField] protected List<TConnection> connections = new();

        public bool IsRoot => isRoot;
        public IEnumerable<TConnection> Connections => connections;

        
        public abstract ConnectionCount InputConnection { get; }
        public abstract ConnectionCount OutputConnection { get; }

        public virtual TNode Clone()
        {
            var clone = (TNode)Instantiate(this);
            clone.isRoot = isRoot;
            clone.connections = connections.ConvertAll(c => c.Clone());
            return clone;
        }
        
#if UNITY_EDITOR
        [HideInInspector] [SerializeField] public Vector2 treePos;
        [HideInInspector] [SerializeField] public string treeGuid;

        string INode.TreeTitle => name;
        
        Vector2 INode.TreePos
        {
            get => treePos;
            set => treePos = value;
        }

        string INode.TreeGuid
        {
            get => treeGuid;
            set => treeGuid = value;
        }
#endif
    }
}