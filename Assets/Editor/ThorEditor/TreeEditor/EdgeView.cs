using System;
using ThorGame.Trees;
using UnityEngine;

namespace ThorEditor.TreeEditor
{
    public class EdgeView : UnityEditor.Experimental.GraphView.Edge
    {
        public NodeView From => output?.node as NodeView;
        public NodeView To => input?.node as NodeView;

        public bool MultipleConnections => (From?.node?.OutputConnection == ConnectionCount.Multi) &&
                                           (To?.node?.InputConnection == ConnectionCount.Multi);
        
        public event Action<ConnectionCollection> OnEdgeSelected;

        public ConnectionCollection Connections { get; } = ScriptableObject.CreateInstance<ConnectionCollection>();

        public override void OnPortChanged(bool isInput)
        {
            base.OnPortChanged(isInput);
            
            if (!isGhostEdge)
            {
                Connections.SetNodes(From?.node, To?.node);
            }
        }
        

        public override void OnSelected()
        {
            Debug.Log(this.viewDataKey);
            Debug.Log(Connections.GetInstanceID());
            OnEdgeSelected?.Invoke(Connections);
        }

        public override void OnUnselected()
        {
            OnEdgeSelected?.Invoke(null);
        }
    }
}