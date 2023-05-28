﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThorGame.Trees;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace ThorEditor.TreeEditor
{
    public class TreeViewElement : GraphView
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
            
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/UI/TreeEditorWindow.uss");
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
            
            //TreeEditorUtility.EnsureTreeHasRoot(tree);
            foreach (var node in tree.AllNodes)
            {
                CreateNodeView(node);
            }
            //Cria as conexões apenas depois de criar todos os nós
            foreach (var node in tree.AllNodes)
            {
                NodeView parentView = FindNodeView(node);
                foreach (var child in node.GetChildren())
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
                            //parentView.node.RemoveChild(childView.node);
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
                        //parentView.node.AddChild(childView.node);    
                    }
                }
            }

            return graphViewChange;
        }


        
        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            var baseType = tree.NodeType();
            var types = TypeCache.GetTypesDerivedFrom(baseType);
            void RecursiveTypeIteration(Type type, Action<Type> preProcess, Action<Type> postProcess)
            {
                preProcess(type);
                foreach (var child in types.Where(t => t.BaseType == type))
                {
                    RecursiveTypeIteration(child, preProcess, postProcess);
                }
                postProcess(type);
            }
            
            Vector2 position = viewTransform.matrix.inverse.MultiplyPoint(evt.localMousePosition);
            
            StringBuilder subPath = new();
            RecursiveTypeIteration(baseType,
                preProcess: type =>
                {
                    if (type.IsAbstract)
                    {
                        subPath.Append(type.Name + '/');
                        evt.menu.AppendSeparator(subPath.ToString());
                    }
                    else
                    {
                        evt.menu.AppendAction($"{type.Name}",
                            a =>
                            {
                                var nodeView = CreateNode(type);
                                nodeView.style.left = position.x;
                                nodeView.style.top = position.y;
                                if (tree.GetRoot() == null) tree.MakeRoot(nodeView.node);
                            });
                    }
                },
                postProcess: type =>
                {
                    if (type.IsAbstract)
                    {
                        int length = type.Name.Length + 1;
                        subPath.Remove(subPath.Length - length, length);
                    }
                }
            );

            /*string subPath = "";
            StringBuilder sb = new();
            foreach (var type in types)
            {
                if (type.IsAbstract)
                {
                    subPath += type.Name + '/';
                    evt.menu.AppendSeparator(subPath);
                }
                
                evt.menu.AppendAction($"[{baseType.Name}] {type.Name}", 
                    a =>
                    {
                        var nodeView = CreateNode(type);
                        nodeView.style.left = position.x;
                        nodeView.style.top = position.y;
                    });
            }*/
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
    }
}