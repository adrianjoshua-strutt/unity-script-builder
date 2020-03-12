using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Reflection;

[CustomPropertyDrawer(typeof(DrawableScriptableObjectList<>), true)]
public class DrawableScriptableObjectListDrawer : PropertyDrawer
{


    /// <summary>
    /// BUG
    /// https://answers.unity.com/questions/661360/finally-a-solution-cant-use-guilayout-stuff-in-pro.html
    /// </summary>

    public int index = 0;

    public List<Type> allItems = new List<Type>();
    public String[] allItemsAsString;

    public Type ItemsType;

    Vector2 scrollPos;

    SerializedProperty selectedProperty;
    int selectedIndex = -1;

    int buttonHeight = 25;
    
    GenericMenu itemSelectPopup;
    GenericMenu itemSelectPopupToShow;
    int itemSelectedPopup = -1;

    SerializedProperty items; 


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        items = property.FindPropertyRelative("Items");
        Color colorOld;
        EditorGUILayout.BeginHorizontal();
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(200), GUILayout.Height((buttonHeight + 2) * items.arraySize + buttonHeight * 2));
            {
                for (int i = 0; i < items.arraySize; i++)
                {
                    SerializedProperty serializedProperty = items.GetArrayElementAtIndex(i);
                    EditorGUILayout.BeginHorizontal();
                    {
                        bool selected = selectedIndex == i;
                        colorOld = GUI.backgroundColor;
                        if (selected)
                        {
                            GUI.backgroundColor = Color.gray;
                        }
                        if (GUILayout.Button(Reflection.SerializedPropertyGetPropertyValue<String>(serializedProperty, "Name"), GUILayout.Height(buttonHeight), GUILayout.ExpandHeight(false)))
                        {
                            if (selected)
                            {
                                selectedProperty = null;
                                selectedIndex = -1;
                            }
                            else
                            {
                                selectedProperty = serializedProperty;
                                selectedIndex = i;
                            }
                        }
                        GUI.backgroundColor = Color.red;
                        if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(buttonHeight)))
                        {
                            items.DeleteArrayElementAtIndex(i);
                            items.DeleteArrayElementAtIndex(i);
                            selectedProperty = null;
                            selectedIndex = -1;
                        }
                        GUI.backgroundColor = colorOld;
                    }
                    EditorGUILayout.EndHorizontal();
                }

                colorOld = GUI.backgroundColor;
                GUI.backgroundColor = Color.green;
                UpdateItems(property);
                if (GUILayout.Button("Add " + ItemsType.Name, GUILayout.Height(buttonHeight)))
                {
                    itemSelectPopupToShow = itemSelectPopup;
                }
                GUI.backgroundColor = colorOld;
                if (itemSelectPopupToShow != null)
                {
                    itemSelectPopupToShow.ShowAsContext();
                    itemSelectPopupToShow = null;
                }

            }
            EditorGUILayout.EndScrollView();

            EditorGUILayout.BeginVertical();
            {
                if (selectedProperty != null)
                {
                    EditorGUILayout.PropertyField(selectedProperty, true, GUILayout.ExpandHeight(false));
                }
            }
            EditorGUILayout.EndVertical();

        }
        EditorGUILayout.EndHorizontal();

    }

    void PopupCallback(object obj)
    {
        itemSelectedPopup = (int)obj;
        itemSelectPopupToShow = null;

        items.InsertArrayElementAtIndex(items.arraySize);
        SerializedProperty added = items.GetArrayElementAtIndex(items.arraySize - 1);
        added.objectReferenceValue = ScriptableObject.CreateInstance(allItems[itemSelectedPopup]);
        items.serializedObject.ApplyModifiedProperties();
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
        itemSelectPopup = new GenericMenu();
        for (int i = 0; i < allItemsAsString.Length; i++)
        {
            itemSelectPopup.AddItem(new GUIContent(allItemsAsString[i]), false, PopupCallback, i);
        }
    }

}
