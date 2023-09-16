using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ThorGame.Trees;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace ThorEditor.TreeEditor
{
    public class ConnectionCollection : ScriptableObject, IEnumerable<IConnection>
    {
        private INode _from, _to;
        public void SetNodes(INode from, INode to)
        {
            if (_from == from && _to == to) return;

            Clear();
            _from = from;
            _to = to;

            if (from == null || to == null) return;

            connections.AddRange(from.GetConnections().Where(connection => connection.To ==  to));
            
            RefreshTypes(from.ConnectionType());
            GuaranteeConnectionCount();
        }
        
        private Type[] _types;
        private void RefreshTypes(Type baseType)
        {
            var derived = TypeCache.GetTypesDerivedFrom(baseType).Where(t => !t.IsAbstract);
            if (!baseType.IsAbstract) derived = derived.Append(baseType);
            _types = derived.ToArray();
            if (_types.Length == 0)
            {
                Debug.LogError("No type derived from " + baseType + " for " + nameof(EdgeView));
            }
        }

        public bool AllowMultiple => _from?.OutputConnection == ConnectionCount.Multi &&
                                     _to?.InputConnection == ConnectionCount.Multi;
            
        [SerializeField] private List<IConnection> connections = new();
        
        public void Load(IConnection connection)
        {
            connections.Add(connection);
        }
            
        private IConnection CreateConnection(Type type)
        {
            var connection = _from.CreateAndRegisterConnection(_to, type);
            return connection;
        }

        private void DestroyConnection(int index, IConnection replacement = null)
        {
            var removed = connections[index];
            if (replacement != null) connections[index] = replacement;
            else connections.RemoveAt(index);
            _from.RemoveChild(removed);
            DestroyImmediate((UnityEngine.Object)removed);
        }

        private void DestroyRange(int fromInclusive, int toExclusive)
        {
            for (int i = toExclusive - 1; i >= fromInclusive; i--)
            {
                DestroyConnection(i);
            }
        }
        
        public void Clear() => DestroyRange(0, connections.Count);

        private void GuaranteeConnectionCount()
        {
            if (connections.Count == 0)
            {
                connections.Add(CreateConnection(_types[0]));
            }
            else if (!AllowMultiple && connections.Count > 1)
            {
                DestroyRange(1, connections.Count);
            }
        }

        public IEnumerator<IConnection> GetEnumerator() => connections.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        [CustomEditor(typeof(ConnectionCollection))]
        public class CollectionEditor : Editor
        {
            private ConnectionCollection _col;

            public override void OnInspectorGUI()
            {
                _col = (ConnectionCollection)target;
                if (_col._from == null || _col._to == null) return;
                
                _col.GuaranteeConnectionCount();
                for (int i = 0; i < _col.connections.Count; i++)
                {
                    DrawEditor(i);
                }

                if (GUILayout.Button("Add"))
                {
                    _col.connections.Add(_col.CreateConnection(_col._types[0]));
                }
            }
            
            private Dictionary<IConnection, Editor> _connectionEditors = new();

            private Editor GetEditor(int index) => index < _col.connections.Count ? GetEditor(_col.connections[index]) : null;
            private Editor GetEditor(IConnection connection)
            {
                if (!_connectionEditors.TryGetValue(connection, out Editor value) || value == null)
                {
                    value = Editor.CreateEditor((UnityEngine.Object)connection);
                    _connectionEditors.Add(connection, value);
                }
                return value;
            }

            private void DestroyEditor(IConnection connection)
            {
                if (_connectionEditors.TryGetValue(connection, out Editor value))
                {
                    Editor.DestroyImmediate(value);
                    _connectionEditors.Remove(connection);
                }
            }

            private void DrawEditor(int index)
            {
                GetEditor(index)?.DrawHeader();
                DrawEditorHeader(index);
                GetEditor(index)?.OnInspectorGUI();
            }

            private void DrawEditorHeader(int index)
            {
                if (index >= _col.connections.Count) return;
                
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Type:");
                DrawTypeSelector(_col._types, index);
                if (index > 0 && GUILayout.Button("Remove"))
                {
                    DestroyEditor(_col.connections[index]);
                    _col.DestroyConnection(index);
                }
                EditorGUILayout.EndHorizontal();
            }
            
            private void DrawTypeSelector(IList<Type> types, int connectionIndex)
            {
                int typeIndex = types.IndexOf(_col.connections[connectionIndex].GetType());
                int newTypeIndex = EditorGUILayout.Popup(typeIndex, types.Select(t => t.Name).ToArray());
                if (typeIndex == newTypeIndex) return;

                IConnection newConnection = _col.CreateConnection(types[newTypeIndex]);
                DestroyEditor(_col.connections[connectionIndex]);
                _col.DestroyConnection(connectionIndex, newConnection);
            }

            private void OnDisable()
            {
                _col = null;
                foreach (var editor in _connectionEditors.Values) Editor.DestroyImmediate(editor);
                _connectionEditors.Clear();
            }
        }
    }
}