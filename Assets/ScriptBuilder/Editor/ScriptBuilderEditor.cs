using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScriptBuilder))]
public class ScriptBuilderEditor : Editor
{

    public override void OnInspectorGUI()
    {
        SerializedProperty serializedProperty = serializedObject.GetIterator();
        if (serializedProperty.NextVisible(true))
        {
            do
            {
                if (serializedProperty.name == "m_Script")
                {
                    continue;
                }
                EditorGUILayout.PropertyField(serializedObject.FindProperty(serializedProperty.name), true, GUILayout.ExpandHeight(false));
            }
            while (serializedProperty.NextVisible(false));
        }
        serializedObject.ApplyModifiedProperties();
    }

}