﻿using System.Collections.Generic;
using System.Linq;
using ThorEditor.UIElements;
using ThorGame.Trees;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ThorEditor.TreeEditor
{
    public class TreeEditorWindow : EditorWindow
    {
        [MenuItem("Window/Tree Editor")]
        private static void ShowWindow()
        {
            CreateWindow();
        }
        private static TreeEditorWindow CreateWindow()
        {
            var window = GetWindow<TreeEditorWindow>();
            window.titleContent = new GUIContent("Tree Editor");
            window.Show();
            return window;
        }

        private TreeViewElement _treeView;
        private ObjectInspectorView _inspectorView;
        private ObjectField _treeSelector;
        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;
            
            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UI/TreeEditorWindow.uxml");
            visualTree.CloneTree(root);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/UI/TreeEditorWindow.uss");
            root.styleSheets.Add(styleSheet);

            _treeView = root.Q<TreeViewElement>();
            _inspectorView = root.Q<ObjectInspectorView>();

            _treeSelector = root.Q<ObjectField>();
            _treeSelector.objectType = typeof(ITree);
            _treeSelector.RegisterValueChangedCallback(e => SelectTree(e.newValue as ITree));

            _treeView.OnNodeSelected += OnNodeSelectionChange;
        }

        private void SelectTree(ITree tree)
        {
            if (_treeSelector.value != (Object)tree)
            {
                _treeSelector.SetValueWithoutNotify((Object)tree);
            }
            _treeView.PopulateView(tree);
        }
        
        
        [UnityEditor.Callbacks.OnOpenAsset]
        private static bool OpenEditorWindow(int instanceID, int line)
        {
            if (EditorUtility.InstanceIDToObject(instanceID) is not ITree tree)
            {
                return false;
            }
            var window = CreateWindow();
            window.SelectTree(tree);
            return true;
        }

        private void OnNodeSelectionChange(INode node)
        {
            _inspectorView.SetSelection(node as Object);
        }

        private void OnConnectionSelectionChange(IEnumerable<IConnection> connections)
        {
            _inspectorView.SetSelection(connections.OfType<Object>());
        }
    }
}