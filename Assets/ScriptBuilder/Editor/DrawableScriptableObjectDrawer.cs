using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomPropertyDrawer(typeof(DrawableScriptableObject), true)]
public class DrawableScriptableObjectDrawer : PropertyDrawer
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

    }

}
