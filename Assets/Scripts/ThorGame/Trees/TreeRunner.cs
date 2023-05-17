using UnityEngine;

namespace ThorGame.Trees
{
    public abstract class TreeRunner<TTree, TData, TReturn> : MonoBehaviour where TTree: ITree<TData, TReturn>
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