using UnityEngine;

namespace ThorGame.Trees
{
    public abstract class TreeRunner<TTree, TData, TReturn, TNode> : MonoBehaviour 
        where TTree: ITypedTree<TNode, TData, TReturn>
        where TNode: ITypedNode<TData, TReturn, TNode>
    {
        [SerializeField] protected TTree tree;
        [SerializeField] protected TData data;

        protected virtual void Awake()
        {
            if (tree is ICloneable<TTree> cloneable)
            {
                tree = cloneable.Clone();
            }
        }

        private void Update()
        {
            tree.Tick(data);
        }
    }
}