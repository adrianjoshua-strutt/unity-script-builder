using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Reflection;

[CustomPropertyDrawer(typeof(DrawableScriptableObjectList<>), true)]
public class DrawableScriptableObjectListDrawer : PropertyDrawer
{

    private bool showItems;

    public int index = 0;

    public List<Type> allItems = new List<Type>();
    public String[] allItemsAsString;

    public Type ItemsType;

    Vector2 scrollPos;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty items = property.FindPropertyRelative("Items");
        showItems = EditorGUILayout.Foldout(showItems, Reflection.SerializedPropertyGetPropertyValue<String>(property, "DisplayName"));
        if (showItems)
        {
            EditorGUI.indentLevel++;
            Color colorOld;
            for (int i = 0; i < items.arraySize; i++)
            {
                SerializedProperty serializedProperty = items.GetArrayElementAtIndex(i);
                bool expanded = serializedProperty.isExpanded;
                if (!expanded) {
                    EditorGUILayout.BeginHorizontal();
                }
                DrawProperty(serializedProperty);
                colorOld = GUI.backgroundColor;
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Remove"))
                {
                    items.DeleteArrayElementAtIndex(i);
                    items.DeleteArrayElementAtIndex(i);
                }
                GUI.backgroundColor = colorOld;
                if (!expanded)
                {
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.BeginHorizontal();
            UpdateItems(property);
            index = EditorGUILayout.Popup(index, allItemsAsString);
            colorOld = GUI.backgroundColor;
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Add...") && allItems.Count > 0)
            {
                items.InsertArrayElementAtIndex(items.arraySize);
                SerializedProperty added = items.GetArrayElementAtIndex(items.arraySize - 1);
                added.objectReferenceValue = ScriptableObject.CreateInstance(allItems[index]);
            }
            GUI.backgroundColor = colorOld;
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;
        }

    }

    public virtual void DrawProperty(SerializedProperty property) {
        EditorGUILayout.BeginHorizontal();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(100), GUILayout.Height(100));
        GUILayout.Label("123");
        EditorGUILayout.EndScrollView();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.PropertyField(property, true);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }

    private void UpdateItems(SerializedProperty property)
    {
        ItemsType = Reflection.SerializedPropertyGetPropertyValue<Type>(property, "ObjectType");
        allItems = Reflection.GetSubTypes(ItemsType);
        List<String> allItemsTemp = new List<String>();
        foreach (Type t in allItems)
        {
            ScriptableObject scriptable = ScriptableObject.CreateInstance(t);
            PropertyInfo propertyInfo = t.GetProperty("FullName");
            allItemsTemp.Add((String)propertyInfo.GetValue(scriptable));
        }
        allItemsAsString = allItemsTemp.ToArray();
    }

}
