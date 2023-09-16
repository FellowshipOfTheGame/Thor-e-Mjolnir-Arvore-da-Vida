using System;
using ThorGame.Trees;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace ThorEditor.TreeEditor
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        private static readonly Color RootColor = new Color(1, 1, 0, .25f); 
        
        public event Action<INode> OnNodeSelected;
        public event Action<INode> MakeRoot;
        
        public Port input, output;
        public INode node;
        private Color _defaultColor;
        public NodeView(INode node)
        {
            this.node = node;
            viewDataKey = node.TreeGuid;
            style.left = node.TreePos.x;
            style.top = node.TreePos.y;

            _defaultColor = style.backgroundColor.value;

            RefreshNodeParameters();

            CreateInputPorts();
            CreateOutputPorts();
        }

        
        public void RefreshNodeParameters()
        {
            title = node.Title;

            if (node.IsRoot)
            {
                capabilities &= ~Capabilities.Deletable;
                style.backgroundColor = RootColor;
            }
            else
            {
                capabilities |= Capabilities.Deletable;
                style.backgroundColor = _defaultColor;
            }
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            evt.menu.AppendAction("Make root", action =>
            {
                MakeRoot?.Invoke(node);
            }, node.IsRoot ? DropdownMenuAction.Status.Disabled : DropdownMenuAction.Status.Normal);
            base.BuildContextualMenu(evt);
        }

        public override Port InstantiatePort(Orientation orientation, Direction direction, Port.Capacity capacity, Type type)
        {
            return Port.Create<EdgeView>(orientation, direction, capacity, type);
        }

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
            node.TreePos = newPos.min;
        }

        public override void OnSelected()
        {
            OnNodeSelected?.Invoke(node);
        }

        public override void OnUnselected()
        {
            OnNodeSelected?.Invoke(null);
        }
    }
}