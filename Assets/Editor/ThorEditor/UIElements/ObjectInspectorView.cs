using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace ThorEditor.UIElements
{
    /// <summary>
    /// VisualElement que renderiza o inspetor de um conjunto de Objects.
    /// </summary>
    public class ObjectInspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<ObjectInspectorView, UxmlTraits>{}

        private readonly Dictionary<Object, Editor> _editors = new();

        public bool IsSelected(Object obj) => _editors.ContainsKey(obj);

        public event Action<Object> OnObjectEdited;

        /// <summary> Limpa todos os Inspectors exibidos. </summary>
        public void ClearSelection()
        {
            Clear();
            foreach (var editor in _editors.Values)
            {
                Object.DestroyImmediate(editor);
            }
            _editors.Clear();
        }

        /// <summary> Adiciona 'obj' aos Inspectors exibidos. </summary>
        private void AddSelection(Object obj)
        {
            if (obj == null) return;
            
            var editor = Editor.CreateEditor(obj);
            
            _editors.Add(obj, editor);
            //var container = new IMGUIContainer(editor.OnInspectorGUI);
            var container = new IMGUIContainer(() =>
            {
                editor.DrawHeader();
                
                EditorGUI.BeginChangeCheck();
                editor.OnInspectorGUI();
                if (EditorGUI.EndChangeCheck())
                {
                    OnObjectEdited?.Invoke(obj);
                }
            });
            Add(container);
        }

        /// <summary> Limpa todos os Inspectors exibidos e muda para 'objects'. </summary>
        public void SetSelection(params Object[] objects) => SetSelection(objects as IEnumerable<Object>);
        /// <summary> Limpa todos os Inspectors exibidos e muda para 'objects'. </summary>
        public void SetSelection(IEnumerable<Object>  objects)
        {
            ClearSelection();
            if (objects == null) return;
            foreach (var obj in objects)
            {
                AddSelection(obj);
            }
        }
    }
}