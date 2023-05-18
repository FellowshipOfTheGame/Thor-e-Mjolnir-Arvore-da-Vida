using System.Collections.Generic;
using UnityEngine;

namespace ThorGame.Trees
{
    public abstract class TypedTree<TTree, TNode, TConnection> : ScriptableObject, ICloneable<TTree>
        where TTree : TypedTree<TTree, TNode, TConnection>
        where TNode : TypedNode<TNode, TConnection>
        where TConnection : TypedConnection<TNode, TConnection>
    {
        [HideInInspector] [SerializeField] protected TNode root;
        [HideInInspector] [SerializeField] protected List<TNode> allNodes = new();
        
        public IEnumerable<TNode> AllNodes => allNodes;
        public TNode Root => root;
        
        public virtual TTree Clone()
        {
            var clone = (TTree)Instantiate(this);
            clone.allNodes = allNodes.ConvertAll(n => n.Clone());
            clone.root = root ? clone.allNodes[allNodes.IndexOf(root)] : null;
            return clone;
        }
    }
}