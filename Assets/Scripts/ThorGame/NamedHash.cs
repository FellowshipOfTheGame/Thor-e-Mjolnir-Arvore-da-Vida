using System;
using UnityEngine;

namespace ThorGame
{
    [Serializable]
    public struct NamedHash
    {
        public string Name;
            
        private int? _hash;
        public int Hash => _hash ?? (_hash = Animator.StringToHash(Name)).Value;

        public static implicit operator NamedHash(string str) => new () { Name = str };
        public static bool operator ==(NamedHash a, NamedHash b) => a.Hash == b.Hash;
        public static bool operator !=(NamedHash a, NamedHash b) => a.Hash != b.Hash;

        public bool Equals(NamedHash other) => Hash == other.Hash;
        public override bool Equals(object obj) => obj is NamedHash other && Equals(other);
        public override int GetHashCode() => Hash;
            
#if UNITY_EDITOR
        [UnityEditor.CustomPropertyDrawer(typeof(NamedHash))]
        private class Drawer : UnityEditor.PropertyDrawer
        {
            public override void OnGUI(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
            {
                var nameProp = property.FindPropertyRelative(nameof(Name));
                UnityEditor.EditorGUI.PropertyField(position, nameProp, label);
            }
        }
#endif
    }
}