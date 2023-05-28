using System;
using System.Collections.Generic;
using ThorGame.Trees;

namespace ThorEditor.TreeEditor
{
    public class EdgeView : UnityEditor.Experimental.GraphView.Edge
    {
        public NodeView From => output.node as NodeView;
        public NodeView To => input.node as NodeView;
        
        public event Action<IEnumerable<IConnection>> OnEdgeSelected;

        private List<IConnection> _connections = new();
        public IReadOnlyCollection<IConnection> Connections => _connections.AsReadOnly();

        public void AddConnection(IConnection connection)
        {
            //TODO NÃO POED ADICIONAR MAIS DE 1 se FROM NAO SUPORTA MAIS DE 1
            _connections.Add(connection);
        }

        public override void OnSelected()
        {
            OnEdgeSelected?.Invoke(Connections);
        }

        public override void OnUnselected()
        {
            OnEdgeSelected?.Invoke(null);
        }
    }
}