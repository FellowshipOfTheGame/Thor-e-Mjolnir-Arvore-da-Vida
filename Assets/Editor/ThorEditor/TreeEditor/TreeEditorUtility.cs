using System;
using System.Collections.Generic;
using System.Reflection;
using ThorGame.Player.HammerControls.Modes;
using ThorGame.Player.HammerControls.ModeSet;
using ThorGame.Trees;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;

namespace ThorEditor.TreeEditor
{
    public static class TreeEditorUtility
    {
        /// <summary>Asserts childType inherits from baseType.</summary>
        private static void AssertInheritance(Type baseType, Type childType, string methodName, string variableName)
        {
            Assert.IsTrue(baseType.IsAssignableFrom(childType),
                $"{methodName} expects {variableName} which inherits from {baseType}, but it is {childType}.");            
        }

        /// <summary>Asserts childType inherits from a baseType which is a generic type without defined parameters.</summary>
        private static void AssertGenericInheritance(Type baseType, Type childType, string methodName,
            string variableName)
        {
            while (childType != null)
            {
                if (childType.IsGenericType && childType.GetGenericTypeDefinition() == baseType) return;
                childType = childType.BaseType;
            }
            Assert.IsTrue(true,
                $"{methodName} expects {variableName} which inherits from {baseType}, but it is {childType}.");            
        }

        private static readonly Type TreeType = typeof(TypedTree<,,>);
        private static readonly Type NodeType = typeof(TypedNode<,>);
        private static readonly FieldInfo RootFieldInfo = TreeType.GetField("root", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo AllNodesFieldInfo = TreeType.GetField("allNodes", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly MethodInfo ListAddMethodInfo = typeof(List<>).GetMethod("Add, ")
        
        public static INode CreateNode(this ITree tree, Type type)
        {
            AssertGenericInheritance(TreeType, tree.GetType(), nameof(CreateNode), nameof(tree));
            AssertGenericInheritance(NodeType, type, nameof(CreateNode), nameof(tree));
            
            var obj = ScriptableObject.CreateInstance(type);
            var node = (INode)obj;
            obj.name = type.Name;
            node.TreeGuid = GUID.Generate().ToString();

            var list = AllNodesFieldInfo.GetValue(tree);
            list.Add(node);
            
            AssetDatabase.AddObjectToAsset(node, tree);
            AssetDatabase.SaveAssets();
            return node;
        }

        public static void DeleteNode<TTree, TNode, TData, TReturn>(this TTree tree, TNode node)
            where TTree: ScriptableObject, ITypedTree<TNode, TData, TReturn>
            where TNode: ScriptableObject, ITypedNode<TData, TReturn, TNode>
        {
            if (tree.AllNodes is not List<TNode> list)
            {
                Debug.LogError("DeleteNode só funciona se AllNodes da árvore for uma lista!");
                return;
            }
            
            list.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }
        
        public static void EnsureTreeHasRoot<TTree, TNode, TData, TReturn>(TTree tree)
            where TTree: ScriptableObject, ITypedTree<TNode, TData, TReturn>
            where TNode: ScriptableObject, ITypedNode<TData, TReturn, TNode>
        {
            if (tree.Root == null)
            {
                var node = tree.CreateNode<TTree, TNode, TData, TReturn>(typeof(TNode));
                tree.Root = node;
                AssetDatabase.SaveAssets();
            }
        }
    }
}