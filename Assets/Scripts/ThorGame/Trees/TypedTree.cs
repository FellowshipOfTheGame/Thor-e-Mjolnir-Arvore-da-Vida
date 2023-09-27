using System.Collections.Generic;
using UnityEngine;

namespace ThorGame.Trees
{
    public abstract class TypedTree<TTree, TNode, TConnection> : ScriptableObject, ITree
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

            Dictionary<TNode, TNode> clones = new();
            clone.root = root.Clone(clones);
            clone.allNodes = new List<TNode>(clones.Values);
            return clone;
        }

        private void OnValidate()
        {
            allNodes.RemoveAll(n => !n);
        }

        IEnumerable<INode> ITree.AllNodes => AllNodes;
    }
}