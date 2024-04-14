using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UniOwl.Editor
{
    [CustomEditor(typeof(ScriptableStructObject<>))]
    public class ScriptableStructObjectEditor : UnityEditor.Editor
    {
        private const string ScriptPropertyName = "m_Script";
        private const string ValuePropertyName = "_value";

        private SerializedProperty scriptProperty;
        private SerializedProperty valueProperty;
        
        protected void OnEnable()
        {
            scriptProperty = serializedObject.FindProperty(ScriptPropertyName);
            valueProperty = serializedObject.FindProperty(ValuePropertyName);
        }

        public override void OnInspectorGUI()
        {
            GUI.enabled = false;
            EditorGUILayout.PropertyField(scriptProperty);
            GUI.enabled = true;
            
            serializedObject.Update();

            using var changeCheck = new EditorGUI.ChangeCheckScope();
            
            foreach (SerializedProperty child in valueProperty.GetChildren())
                EditorGUILayout.PropertyField(child);

            if (changeCheck.changed)
                serializedObject.ApplyModifiedProperties();
        }
        
        private static IEnumerable<SerializedProperty> GetChildren(SerializedProperty property)
        {
            property = property.Copy();

            SerializedProperty nextElement = property.Copy();
            
            if (!nextElement.NextVisible(false))
                nextElement = null;

            property.NextVisible(true);
            
            do
            {
                if (SerializedProperty.EqualContents(property, nextElement))
                    yield break;

                yield return property;

            } while (property.NextVisible(false));
        }
    }
}