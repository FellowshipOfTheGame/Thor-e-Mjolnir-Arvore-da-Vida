using System;
using System.Collections.Generic;
using ThorGame.Trees;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

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
            viewDataKey = node.TreeGuid;
            style.left = node.TreePos.x;
            style.top = node.TreePos.y;

            if (node.IsRoot)
            {
                capabilities &= ~Capabilities.Deletable;
                style.backgroundColor = new StyleColor(Color.yellow);
            }

            CreateInputPorts();
            CreateOutputPorts();
        }

        public override string title => node.TreeTitle;
        
        private void CreateInputPorts()
        {
            if (node.InputConnection == ConnectionCount.None || node.IsRoot) return;

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