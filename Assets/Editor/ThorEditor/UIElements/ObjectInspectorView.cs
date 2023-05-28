using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ThorEditor.UIElements
{
    /// <summary>
    /// VisualElement que renderiza o inspetor de um conjunto de Objects.
    /// </summary>
    public class ObjectInspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<ObjectInspectorView, UxmlTraits>{}

        private readonly Dictionary<Object, UnityEditor.Editor> _editors = new();

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
        public void AddSelection(Object obj)
        {
            var editor = UnityEditor.Editor.CreateEditor(obj);
            _editors.Add(obj, editor);
            var container = new IMGUIContainer(() => editor.OnInspectorGUI());
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