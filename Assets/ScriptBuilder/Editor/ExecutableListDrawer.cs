using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(ExecutableList))]
public class ExecutableListDrawer : PropertyDrawer
{

    private bool showExecutables;

    public int index = 0;

    public List<Type> allExecutables = getAllExecutables();
    public String[] allExecutablesAsString;


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty executables = property.FindPropertyRelative("Executables");
        showExecutables = EditorGUILayout.Foldout(showExecutables, "Executables");
        if (showExecutables)
        {
            EditorGUI.indentLevel++;
            Color colorOld;
            for (int i = 0; i < executables.arraySize; i++)
            {
                SerializedProperty serializedProperty = executables.GetArrayElementAtIndex(i);
                bool expanded = EditorGUILayout.PropertyField(serializedProperty, true);
                colorOld = GUI.backgroundColor;
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Remove"))
                {
                    executables.DeleteArrayElementAtIndex(i);
                    executables.DeleteArrayElementAtIndex(i);
                }
                GUI.backgroundColor = colorOld;
            }
            EditorGUILayout.BeginHorizontal();
            UpdateExecutables();
            index = EditorGUILayout.Popup(index, allExecutablesAsString);
            colorOld = GUI.backgroundColor;
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Add...") && allExecutables.Count > 0)
            {
                executables.InsertArrayElementAtIndex(executables.arraySize);
                SerializedProperty added = executables.GetArrayElementAtIndex(executables.arraySize - 1);
                added.objectReferenceValue = ScriptableObject.CreateInstance(allExecutables[index]);
            }
            GUI.backgroundColor = colorOld;
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;
        }

        //https://docs.unity3d.com/ScriptReference/EditorGUILayout.Popup.html

    }

    private static List<Type> getAllExecutables() {
        return SlimNetSubTypeReflector.GetSubTypes<Executable>();
    }

    private void UpdateExecutables() {
        allExecutables = getAllExecutables();
        List<String> allExecutablesTemp = new List<String>();
        foreach (Type t in allExecutables) {
            ScriptableObject scriptable = ScriptableObject.CreateInstance(t);
            MethodInfo methodInfo = t.GetMethod("getFullName");
            allExecutablesTemp.Add((String)methodInfo.Invoke(scriptable,null));
        }
        allExecutablesAsString = allExecutablesTemp.ToArray();
    }

}
