using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(DrawableScriptableObjectException), true)]
public class DrawableScriptableObjectExceptionDrawer : PropertyDrawer
{
    // Cached scriptable object editor
    private Editor editor = null;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (!editor)
            Editor.CreateCachedEditor(property.objectReferenceValue, null, ref editor);

        if (!editor)
        {
            return;
        }

        editor.OnInspectorGUI();

        bool hasException = Reflection.SerializedPropertyGetPropertyValue<bool>(property, "HasException");

        if (hasException)
        {
            string exception = Reflection.SerializedPropertyGetPropertyValue<string>(property, "ThrownException");
            EditorGUILayout.HelpBox(exception, MessageType.Error);
        }

    }

}
