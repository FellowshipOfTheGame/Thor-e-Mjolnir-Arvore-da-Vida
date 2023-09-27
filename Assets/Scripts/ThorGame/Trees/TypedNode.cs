using System;
using System.Collections.Generic;
using UnityEngine;

namespace ThorGame.Trees
{
    public abstract class TypedNode<TNode, TConnection> : ScriptableObject, INode
        where TNode : TypedNode<TNode, TConnection>
        where TConnection: TypedConnection<TNode, TConnection>
    {
        [HideInInspector] [SerializeField] protected bool isRoot;
        [SerializeField] protected List<TConnection> connections = new();

        public bool IsRoot => isRoot;
        public IEnumerable<TConnection> Connections => connections;

        
        public abstract ConnectionCount InputConnection { get; }
        public abstract ConnectionCount OutputConnection { get; }

        public virtual TNode Clone(Dictionary<TNode, TNode> clones)
        {
            var clone = (TNode)Instantiate(this);
            clones.Add((TNode)this, clone);
            
            clone.connections.Clear();
            foreach (var connection in connections)
            {
                if (!clones.TryGetValue(connection.To, out var neighborClone))
                {
                    neighborClone = connection.To.Clone(clones);
                }
                clone.connections.Add(connection.Clone(clone, neighborClone));
            }
            
            clone.isRoot = isRoot;
            
            return clone;
        }
        
        public virtual string Title => name;

        private void OnValidate()
        {
            connections.RemoveAll(c => !c);
        }


#if UNITY_EDITOR
        [HideInInspector] [SerializeField] public Vector2 treePos;
        [HideInInspector] [SerializeField] public string treeGuid;

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