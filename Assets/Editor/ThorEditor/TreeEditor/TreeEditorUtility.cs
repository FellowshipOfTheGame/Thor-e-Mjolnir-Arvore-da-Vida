using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ThorGame.Trees;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ThorEditor.TreeEditor
{
    public static class TreeEditorUtility
    {
        private static readonly Type GenericTreeType = typeof(TypedTree<,,>);
        private static readonly Type GenericNodeType = typeof(TypedNode<,>);
        private static readonly Type GenericConnectionType = typeof(TypedConnection<,>);
        
        private static FieldInfo RootFieldInfo(Type treeType) => treeType.GetField("root", BindingFlags.NonPublic | BindingFlags.Instance);
        private static FieldInfo AllNodesFieldInfo(Type treeType) => treeType.GetField("allNodes", BindingFlags.NonPublic | BindingFlags.Instance);

        private static FieldInfo NodeIsRootFieldInfo(Type nodeType) => nodeType.GetField("isRoot", BindingFlags.NonPublic | BindingFlags.Instance);
        private static FieldInfo NodeConnectionsFieldInfo(Type nodeType) => nodeType.GetField("connections", BindingFlags.NonPublic | BindingFlags.Instance);
        
        private static FieldInfo ConnectionFromFieldInfo(Type connectionType) => connectionType.GetField("from", BindingFlags.NonPublic | BindingFlags.Instance);
        private static FieldInfo ConnectionToFieldInfo(Type connectionType) => connectionType.GetField("to", BindingFlags.NonPublic | BindingFlags.Instance);
        
        private static MethodInfo ListAddMethodInfo(Type itemType) => typeof(List<>).MakeGenericType(itemType).GetMethod("Add", new []{itemType});
        private static MethodInfo ListRemoveMethodInfo(Type itemType) => typeof(List<>).MakeGenericType(itemType).GetMethod("Remove", new []{itemType});


        public static Type ConnectionType(this ITree tree) => ReflectionUtility.GetBaseTypeWithGenericDef(GenericTreeType, tree.GetType()).GetGenericArguments()[2];
        public static Type ConnectionType(this INode node) => ReflectionUtility.GetBaseTypeWithGenericDef(GenericNodeType, node.GetType()).GetGenericArguments()[1];
        public static Type NodeType(this ITree tree) => ReflectionUtility.GetBaseTypeWithGenericDef(GenericTreeType, tree.GetType()).GetGenericArguments()[1];
        
        
        public static INode CreateNode(this ITree tree, Type nodeType)
        {
            Type treeInstanceType = tree.GetType();
            ReflectionUtility.AssertGenericInheritance(GenericTreeType, treeInstanceType, nameof(CreateNode), nameof(tree));
            ReflectionUtility.AssertGenericInheritance(GenericNodeType, nodeType, nameof(CreateNode), nameof(nodeType));
            
            var obj = ScriptableObject.CreateInstance(nodeType);
            var node = (INode)obj;
            obj.name = nodeType.Name;
            node.TreeGuid = GUID.Generate().ToString();

            var addMethod = ListAddMethodInfo(tree.NodeType());
            var list = AllNodesFieldInfo(treeInstanceType).GetValue(tree);
            addMethod.Invoke(list, new [] {(object)node});
            
            AssetDatabase.AddObjectToAsset(node as Object, tree as Object);
            AssetDatabase.SaveAssets();
            return node;
        }

        public static void DeleteNode(this ITree tree, INode node)
        {
            Type treeInstanceType = tree.GetType();
            ReflectionUtility.AssertGenericInheritance(GenericTreeType, treeInstanceType, nameof(DeleteNode), nameof(tree));
            ReflectionUtility.AssertGenericInheritance(GenericNodeType, node.GetType(), nameof(DeleteNode), nameof(tree));
            
            var removeMethod = ListRemoveMethodInfo(tree.NodeType());
            var list = AllNodesFieldInfo(treeInstanceType).GetValue(tree);
            removeMethod.Invoke(list, new [] {(object)node});

            AssetDatabase.RemoveObjectFromAsset(node as Object);
            AssetDatabase.SaveAssets();
        }
        
        public static IEnumerable<IConnection> GetConnections(this INode node)
        {
            Type nodeInstanceType = node.GetType();
            ReflectionUtility.AssertGenericInheritance(GenericNodeType, nodeInstanceType, nameof(GetConnections), nameof(node));

            var connections = NodeConnectionsFieldInfo(nodeInstanceType).GetValue(node);
            foreach (object c in (IEnumerable)connections)
            {
                yield return (IConnection)c;
            }
        }
        
        public static IEnumerable<INode> GetChildren(this INode node)=>node.GetConnections().Select(c => c.To).Distinct();

        public static INode GetRoot(this ITree tree)
        {
            Type treeInstanceType = tree.GetType();
            ReflectionUtility.AssertGenericInheritance(GenericTreeType, treeInstanceType, nameof(GetRoot), nameof(tree));

            var root = RootFieldInfo(treeInstanceType).GetValue(tree) as INode;
            return root;
        }

        public static void MakeRoot(this ITree tree, INode node)
        {
            Type treeInstanceType = tree.GetType();
            ReflectionUtility.AssertGenericInheritance(GenericTreeType, treeInstanceType, nameof(MakeRoot), nameof(tree));
            if (node != null) ReflectionUtility.AssertGenericInheritance(GenericNodeType, node.GetType(), nameof(MakeRoot), nameof(tree));

            //Update current root
            var curRoot = RootFieldInfo(treeInstanceType).GetValue(tree);
            if (curRoot != null)
            {
                NodeIsRootFieldInfo(curRoot.GetType()).SetValue(curRoot, false);
            }
            
            //Set new root
            RootFieldInfo(treeInstanceType).SetValue(tree, node);
            if (node != null)
            {
                NodeIsRootFieldInfo(node.GetType()).SetValue(node, true);
            }
        }
        
        public static void AddChild(this INode parent, IConnection child)
        {
            Type parentType = parent.GetType();
            Type childType = child.GetType();
            ReflectionUtility.AssertGenericInheritance(GenericNodeType, parentType, nameof(AddChild), nameof(parent));
            ReflectionUtility.AssertGenericInheritance(GenericConnectionType, childType, nameof(AddChild), nameof(child));

            var addMethod = ListAddMethodInfo(parent.ConnectionType());
            var connections = NodeConnectionsFieldInfo(parentType).GetValue(parent);
            addMethod.Invoke(connections, new []{child as object});
            
            AssetDatabase.AddObjectToAsset(child as Object, parent as Object);
            AssetDatabase.SaveAssets();
        }

        public static void RemoveChild(this INode parent, IConnection child)
        {
            Type parentType = parent.GetType();
            Type childType = child.GetType();
            ReflectionUtility.AssertGenericInheritance(GenericNodeType, parentType, nameof(RemoveChild), nameof(parent));
            ReflectionUtility.AssertGenericInheritance(GenericConnectionType, childType, nameof(RemoveChild), nameof(child));

            var removeMethod = ListRemoveMethodInfo(parent.ConnectionType());
            var connections = NodeConnectionsFieldInfo(parentType).GetValue(parent);
            removeMethod.Invoke(connections, new[] {child as object});
            
            AssetDatabase.RemoveObjectFromAsset(child as Object);
            AssetDatabase.SaveAssets();
        }

        public static IConnection CreateAndRegisterConnection(this INode from, INode to, Type connectionType)
        {
            Type fromType = from.GetType();
            Type toType = to.GetType();
            ReflectionUtility.AssertGenericInheritance(GenericNodeType, fromType, nameof(CreateAndRegisterConnection), nameof(from));
            ReflectionUtility.AssertGenericInheritance(GenericNodeType, toType, nameof(CreateAndRegisterConnection), nameof(to));
            ReflectionUtility.AssertGenericInheritance(GenericConnectionType, connectionType, nameof(CreateAndRegisterConnection), nameof(connectionType));
            
            var obj = ScriptableObject.CreateInstance(connectionType);
            obj.name = connectionType.Name;
            
            var connection = (IConnection) obj;
            ConnectionFromFieldInfo(connectionType).SetValue(connection, from);
            ConnectionToFieldInfo(connectionType).SetValue(connection, to);

            from.AddChild(connection);
            return connection;
        }
    }
}