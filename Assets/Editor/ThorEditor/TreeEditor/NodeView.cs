using System;
using ThorGame.Trees;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace ThorEditor.TreeEditor
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public Action<INode> OnNodeSelected;
        
        public Port input, output;
        public INode node;
        public NodeView(INode node)
        {
            this.node = node;
            this.title = node.TreeTitle;
            this.viewDataKey = node.TreeGuid;
            style.left = node.TreePos.x;
            style.top = node.TreePos.y;

            CreateInputPorts();
            CreateOutputPorts();
        }

        private void CreateInputPorts()
        {
            if (node.InputConnection == ConnectionCount.None) return;

            var capacity = (node.InputConnection == ConnectionCount.Single) ? Port.Capacity.Single : Port.Capacity.Multi;
            input = InstantiatePort(Orientation.Horizontal, Direction.Input, capacity, typeof(bool));
            input.portName = "";
            inputContainer.Add(input);
        }

        private void CreateOutputPorts()
        {
            if (node.OutputConnection == ConnectionCount.None) return;
            
            var capacity = (node.OutputConnection == ConnectionCount.Single) ? Port.Capacity.Single : Port.Capacity.Multi;
            output = InstantiatePort(Orientation.Horizontal, Direction.Output, capacity, typeof(bool));
            output.portName = "";
            outputContainer.Add(output);
        }

        public override bool IsSelectable() => base.IsSelectable() && !node.IsRoot;

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            node.TreePos = new Vector2(newPos.x, newPos.y);
        }

        public override void OnSelected()
        {
            OnNodeSelected?.Invoke(node);
        }
    }
}