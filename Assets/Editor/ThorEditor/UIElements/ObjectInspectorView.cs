using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace ThorEditor.UIElements
{
    /// <summary>
    /// VisualElement que renderiza o inspetor de um Objects.
    /// </summary>
    public class ObjectInspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<ObjectInspectorView, UxmlTraits>{}

        private Object _obj;
        private Editor _editor;

        public bool IsSelected(Object obj) => _obj = obj;

        public event Action<Object> OnObjectEdited;

        public void ClearSelection()
        {
            Clear();
            Object.DestroyImmediate(_editor);
            _obj = null;
        }

        private void AddSelection(Object obj)
        {
            if (obj == null) return;
            
            var editor = Editor.CreateEditor(obj);
            _editor = editor;
            var container = new IMGUIContainer(() =>
            {
                //editor.DrawHeader();
                
                EditorGUI.BeginChangeCheck();
                editor.OnInspectorGUI();
                if (EditorGUI.EndChangeCheck())
                {
                    OnObjectEdited?.Invoke(obj);
                }
            });
            Add(container);
        }

        public void SetSelection(Object obj)
        {
            ClearSelection();
            AddSelection(obj);
        }
    }
}