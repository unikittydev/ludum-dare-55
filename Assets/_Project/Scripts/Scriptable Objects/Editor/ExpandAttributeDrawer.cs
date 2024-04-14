using UnityEngine;
using UnityEditor;

namespace UniOwl.Editor
{
    [CustomPropertyDrawer(typeof(ExpandAttribute))]
    public class ExpandAttributeDrawer : PropertyDrawer
    {
        private UnityEditor.Editor editor;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Draw label
            EditorGUI.PropertyField(position, property, label, true);

            // Draw foldout arrow
            if (property.objectReferenceValue != null)
            {
                property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none);
            }

            // Draw foldout properties
            if (property.isExpanded)
            {
                // Make child fields be indented
                EditorGUI.indentLevel++;

                // Draw object properties
                if (!editor)
                    UnityEditor.Editor.CreateCachedEditor(property.objectReferenceValue, null, ref editor);
                editor.OnInspectorGUI();

                // Set indent back to what it was
                EditorGUI.indentLevel--;
            }
        }
    }
}