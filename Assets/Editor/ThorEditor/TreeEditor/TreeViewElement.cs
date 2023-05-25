using System;
using System.Collections.Generic;
using System.Linq;
using ThorGame.Trees;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace ThorEditor.TreeEditor
{
    /*public class TreeViewElement : GraphView
    {
        public new class UxmlFactory : UxmlFactory<TreeViewElement, UxmlTraits>{}

        public Action<INode> OnNodeSelected;

        public ITree tree;
        public TreeViewElement()
        {
            Insert(0, new GridBackground());
            
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/TreeEditorWindow.uss");
            styleSheets.Add(styleSheet);
        }

        private NodeView FindNodeView(INode node) => GetNodeByGuid(node.TreeGuid) as NodeView;
        
        public void PopulateView(ITree tree)
        {
            this.tree = tree;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;
            
            if (tree == null)
            {
                return;
            }
            
            TreeEditorUtility.EnsureTreeHasRoot(tree);
            foreach (var node in tree.AllNodes)
            {
                CreateNodeView(node);
            }
            //After creating all nodes, we can create edges
            foreach (var node in tree.AllNodes)
            {
                NodeView parentView = FindNodeView(node);
                foreach (var child in TreeEditorUtility.GetChildren(node))
                {
                    NodeView childView = FindNodeView(child);
                    Edge edge = parentView.output.ConnectTo(childView.input);
                    AddElement(edge);
                }
            }
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.Where(endPort =>
                endPort.direction != startPort.direction &&
                endPort.node != startPort.node).ToList();
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if (graphViewChange.elementsToRemove != null)
            {
                foreach (var element in graphViewChange.elementsToRemove)
                {
                    if (element is NodeView nodeView)
                    {
                        tree.DeleteNode(nodeView.node);
                        continue;
                    }

                    if (element is Edge edge)
                    {
                        if (edge.output.node is NodeView parentView && edge.input.node is NodeView childView)
                        {
                            TreeEditorUtility.RemoveChild(parentView.node, childView.node);
                        }
                    }
                }
            }

            if (graphViewChange.edgesToCreate != null)
            {
                foreach (var edge in graphViewChange.edgesToCreate)
                {
                    if (edge.output.node is NodeView parentView && edge.input.node is NodeView childView)
                    {
                        BehaviorTreeEditorUtility.AddChild(parentView.node, childView.node);    
                    }
                }
            }

            return graphViewChange;
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            Vector2 position = viewTransform.matrix.inverse.MultiplyPoint(evt.localMousePosition);
            void AppendActionsForBaseType(Type baseType)
            {
                evt.menu.AppendSeparator();
                var types = TypeCache.GetTypesDerivedFrom(baseType).Where(t => !t.IsAbstract);
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"[{baseType.Name}] {type.Name}", 
                        a =>
                        {
                            var nodeView = CreateNode(type);
                            nodeView.style.left = position.x;
                            nodeView.style.top = position.y;
                        });
                }
            }
            AppendActionsForBaseType(typeof(CompositeNode));
            AppendActionsForBaseType(typeof(DecoratorNode));
            AppendActionsForBaseType(typeof(LeafNode));
        }

        private NodeView CreateNode(System.Type type)
        {
            INode node = tree.CreateNode(type);
            return CreateNodeView(node);
        }

        private NodeView CreateNodeView(INode node)
        {
            NodeView nodeView = new NodeView(node);
            AddElement(nodeView);
            nodeView.OnNodeSelected = OnNodeSelected; 
            return nodeView;
        }
    }*/
}