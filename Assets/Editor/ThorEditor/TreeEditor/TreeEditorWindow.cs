using ThorEditor.UIElements;
using ThorGame.Player.HammerControls.ModeSet;
using ThorGame.Trees;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace ThorEditor.TreeEditor
{
    public class TreeEditorWindowWrapper : EditorWindow
    {
        
    }
    
    public class TreeEditorWindow<TTree, TNode, TConnection> : EditorWindow
        where TTree : TypedTree<TTree, TNode, TConnection>
        where TNode : TypedNode<TNode, TConnection>
        where TConnection : TypedConnection<TNode, TConnection> 
    {
        /*[MenuItem("Window/Hammer Mode Set")]
        private static void ShowWindow()
        {
            CreateWindow();
        }
        private static TreeEditorWindow<TTree, TNode, TConnection> CreateWindow()
        {
            var window = GetWindow<TreeEditorWindow<TTree, TNode, TConnection>>();
            window.titleContent = new GUIContent("Hammer Mode Set Editor");
            window.Show();
            return window;
        }

        private TreeViewElement _treeView;
        private InspectorView _inspectorView;
        private ObjectField _treeSelector;
        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;
            
            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/TreeEditorWindow.uxml");
            visualTree.CloneTree(root);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/TreeEditorWindow.uss");
            root.styleSheets.Add(styleSheet);

            _treeView = root.Q<TreeViewElement>();
            _inspectorView = root.Q<InspectorView>();

            _treeSelector = root.Q<ObjectField>();
            _treeSelector.objectType = typeof(Tree);
            _treeSelector.RegisterValueChangedCallback(e => SelectTree(e.newValue as Tree));

            _treeView.OnNodeSelected += OnNodeSelectionChange;
        }

        private void SelectTree(Tree tree)
        {
            if (_treeSelector.value != tree)
            {
                _treeSelector.SetValueWithoutNotify(tree);
            }
            _treeView.PopulateView(tree);
        }
        
        
        [UnityEditor.Callbacks.OnOpenAsset]
        private static bool OpenEditorWindow(int instanceID, int line)
        {
            if (EditorUtility.InstanceIDToObject(instanceID) is not Tree tree)
            {
                return false;
            }
            var window = CreateWindow();
            window.SelectTree(tree);
            return true;
        }

        private void OnNodeSelectionChange(TypedNode node)
        {
            _inspectorView.UpdateSelection(node as Object);
        }*/
    }
}