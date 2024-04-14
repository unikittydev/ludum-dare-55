using UniOwl.UI;
using UnityEditor;

[CustomEditor(typeof(UniButton))]
public class UniButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_image"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_text"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("hoverClip"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("clickClip"));
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_defaultImageColor"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_hoverImageColor"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_clickImageColor"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_defaultTextColor"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_hoverTextColor"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_clickTextColor"));

        serializedObject.ApplyModifiedProperties();
        
        base.OnInspectorGUI();
    }
}
