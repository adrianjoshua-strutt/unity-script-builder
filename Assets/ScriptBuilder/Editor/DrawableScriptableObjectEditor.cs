using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DrawableScriptableObject), true)]
public class DrawableScriptableObjectEditor : Editor
{

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty serializedProperty = serializedObject.GetIterator();
        if (serializedProperty.NextVisible(true))
        {
            do
            {
                if (serializedProperty.name == "m_Script") {
                    continue;
                }
                EditorGUILayout.PropertyField(serializedObject.FindProperty(serializedProperty.name), true);
            }
            while (serializedProperty.NextVisible(false));
        }
        serializedObject.ApplyModifiedProperties();
    }

}