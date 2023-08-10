using System.Linq;
using UnityEngine;

namespace ThorGame
{
    [System.Serializable]
    public struct VirtualButton
    {
        public string Name;

        public bool Held => Input.GetButton(Name);
        public bool Down => Input.GetButtonDown(Name);
        public bool Up => Input.GetButtonUp(Name);
        
#if UNITY_EDITOR
        [UnityEditor.CustomPropertyDrawer(typeof(VirtualButton))]
        private class Drawer : UnityEditor.PropertyDrawer
        {
            private GUIContent[] GetButtonList(UnityEditor.SerializedProperty arrayProp)
            {
                var buttons = new GUIContent[arrayProp.arraySize];
                for (int i = 0; i < arrayProp.arraySize; i++)
                {
                    var axis = arrayProp.GetArrayElementAtIndex(i);
                    string name = axis.FindPropertyRelative("m_Name").stringValue;
                    buttons[i] = new GUIContent(name);
                }
                return buttons;
            }
            
            public override void OnGUI(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
            {
                var nameProp = property.FindPropertyRelative(nameof(Name));
                
                var inputManager = UnityEditor.AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];
                UnityEditor.SerializedObject obj = new(inputManager);
                var axisArray = GetButtonList(obj.FindProperty("m_Axes"));

                int curIndex = axisArray.ToList().FindIndex(axis => axis.text == nameProp.stringValue);
                if (curIndex < 0) curIndex = 0;
                curIndex = UnityEditor.EditorGUI.Popup(position, label, curIndex, axisArray);

                nameProp.stringValue = axisArray[curIndex].text;
            }
        }
#endif
    }
}