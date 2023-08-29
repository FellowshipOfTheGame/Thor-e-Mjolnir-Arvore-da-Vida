using UnityEngine;
using UnityEngine.EventSystems;

namespace ThorGame
{
    [System.Serializable]
    public struct InputCondition
    {
        //Isso aqui seria tudo default se a gente tivesse usando o sistema de input novo :(
        private enum Type {Key, Mouse, Virtual}
        [SerializeField] private Type type;
        
        private enum Mode {Down, Up, Held}
        [SerializeField] private Mode mode;

        [SerializeField] private KeyCode key;
        [SerializeField] private PointerEventData.InputButton mouseButton;
        [SerializeField] private VirtualButton virtualButton;

        public bool Condition() => mode switch
        {
            Mode.Down => type switch
            {
                Type.Key => Input.GetKeyDown(key),
                Type.Mouse => Input.GetMouseButtonDown((int)mouseButton),
                Type.Virtual => virtualButton.Down,
                _ => false
            },
            Mode.Up => type switch
            {
                Type.Key => Input.GetKeyUp(key),
                Type.Mouse => Input.GetMouseButtonUp((int)mouseButton),
                Type.Virtual => virtualButton.Up,
                _ => false
            },
            Mode.Held => type switch
            {
                Type.Key => Input.GetKey(key),
                Type.Mouse => Input.GetMouseButton((int)mouseButton),
                Type.Virtual => virtualButton.Held,
                _ => false
            },
            _ => false
        };

        public static implicit operator bool(InputCondition condition) => condition.Condition();

#if UNITY_EDITOR
        [UnityEditor.CustomPropertyDrawer(typeof(InputCondition))]
        private class Drawer : UnityEditor.PropertyDrawer
        {
            public override float GetPropertyHeight(UnityEditor.SerializedProperty property, GUIContent label)
            {
                float baseHeight = base.GetPropertyHeight(property, label);
                return property.isExpanded ? 4 * baseHeight + 3 * UnityEditor.EditorGUIUtility.standardVerticalSpacing : baseHeight;
            }

            public override void OnGUI(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
            {
                float baseHeight = UnityEditor.EditorGUIUtility.singleLineHeight;
                Rect lineRect = new Rect(position) { height = baseHeight };
                property.isExpanded = UnityEditor.EditorGUI.Foldout(lineRect, property.isExpanded, label);
                if (!property.isExpanded) return;
                
                var typeProp = property.FindPropertyRelative(nameof(type));
                var modeProp = property.FindPropertyRelative(nameof(mode));

                var valuePropName = typeProp.enumValueIndex switch
                {
                    (int)Type.Key => nameof(key),
                    (int)Type.Mouse => nameof(mouseButton),
                    (int)Type.Virtual => nameof(virtualButton),
                    _ => null
                };
                if (valuePropName == null) return;
                var valueProp = property.FindPropertyRelative(valuePropName);

                lineRect.y += baseHeight + UnityEditor.EditorGUIUtility.standardVerticalSpacing;
                UnityEditor.EditorGUI.PropertyField(lineRect, typeProp);
                lineRect.y += baseHeight + UnityEditor.EditorGUIUtility.standardVerticalSpacing;
                UnityEditor.EditorGUI.PropertyField(lineRect, modeProp);
                lineRect.y += baseHeight + UnityEditor.EditorGUIUtility.standardVerticalSpacing;
                UnityEditor.EditorGUI.PropertyField(lineRect, valueProp);
            }
        }
#endif
    }
}