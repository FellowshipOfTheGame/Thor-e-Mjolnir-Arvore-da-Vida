using UnityEngine;
using UnityEngine.UIElements;

namespace ThorEditor.UIElements
{
    /// <summary>
    /// VisualElement que renderiza o inspetor de um Object.
    /// </summary>
    public class InspectorView : InspectorView<Object> {}

    /// <summary>
    /// VisualElement que renderiza o inspetor de um Object de tipo T.
    /// </summary>
    /// <typeparam name="T">Tipo base que pode ser renderizado pelo inspetor.</typeparam>
    public class InspectorView<T> : VisualElement where T: Object
    {
        public new class UxmlFactory : UxmlFactory<InspectorView<T>, UxmlTraits>{}

        private UnityEditor.Editor _editor;
        /// <summary> Atualiza o objeto a ser mostrado pelo editor. </summary>
        public void UpdateSelection(T obj)
        {
            Clear();
            UnityEngine.Object.DestroyImmediate(_editor);            
            _editor = UnityEditor.Editor.CreateEditor(obj);
            IMGUIContainer container = new IMGUIContainer(() => _editor.OnInspectorGUI());
            Add(container);
        }
    }
}