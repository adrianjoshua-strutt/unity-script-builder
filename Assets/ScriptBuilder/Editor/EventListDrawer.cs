using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(EventList))]
public class EventListDrawer : PropertyDrawer
{

    private bool showEvents;

    public int index = 0;

    public List<Type> allEvents = getAllEvents();
    public String[] allEventsAsString;


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty events = property.FindPropertyRelative("Events");
        showEvents = EditorGUILayout.Foldout(showEvents, "Events");
        if (showEvents)
        {
            EditorGUI.indentLevel++;
            Color colorOld;
            for (int i = 0; i < events.arraySize; i++)
            {
                SerializedProperty serializedProperty = events.GetArrayElementAtIndex(i);
                bool expanded = EditorGUILayout.PropertyField(serializedProperty, true);
                colorOld = GUI.backgroundColor;
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Remove"))
                {
                    events.DeleteArrayElementAtIndex(i);
                    events.DeleteArrayElementAtIndex(i);
                }
                GUI.backgroundColor = colorOld;
            }
            EditorGUILayout.BeginHorizontal();
            UpdateEvent();
            index = EditorGUILayout.Popup(index, allEventsAsString);
            colorOld = GUI.backgroundColor;
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Add...") && allEvents.Count > 0)
            {
                events.InsertArrayElementAtIndex(events.arraySize);
                SerializedProperty added = events.GetArrayElementAtIndex(events.arraySize - 1);
                added.objectReferenceValue = ScriptableObject.CreateInstance(allEvents[index]);
            }
            GUI.backgroundColor = colorOld;
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;
        }

        //https://docs.unity3d.com/ScriptReference/EditorGUILayout.Popup.html

    }

    private static List<Type> getAllEvents()
    {
        return SlimNetSubTypeReflector.GetSubTypes<Event>();
    }

    private void UpdateEvent()
    {
        allEvents = getAllEvents();
        List<String> allEventsTemp = new List<String>();
        foreach (Type t in allEvents)
        {
            ScriptableObject scriptable = ScriptableObject.CreateInstance(t);
            MethodInfo methodInfo = t.GetMethod("getFullName");
            allEventsTemp.Add((String)methodInfo.Invoke(scriptable, null));
        }
        allEventsAsString = allEventsTemp.ToArray();
    }

}
