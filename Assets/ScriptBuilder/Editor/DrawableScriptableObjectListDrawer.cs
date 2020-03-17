using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomPropertyDrawer(typeof(DrawableScriptableObjectList<>), true)]
public class DrawableScriptableObjectListDrawer : PropertyDrawer
{

    public int index = 0;

    public List<Type> allItems = new List<Type>();
    public String[] allItemsAsString;

    public Type ItemsType;

    Vector2 scrollPos;

    SerializedProperty selectedProperty;
    int selectedIndex = -1;

    GenericMenu itemSelectPopup;
    GenericMenu itemSelectPopupToShow;
    int itemSelectedPopup = -1;

    SerializedProperty items;

    private int _buttonHeight;

    private bool _allowUserToRemoveItems;
    private bool _allowUserToAddItems;

    //BUG !!! Wenn eine Executable Klasse gelöscht wird, dann funktioniert es nicht mehr. Das Executable besteht weiter in der LIste, kann aber nicht mehr geladen
    //werden da die Klasse nicht mehr existiert 

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (ItemsType == null || allItemsAsString == null || allItems.Count == 0)
        {
            UpdateItems(property);
            UpdateGraphicSettings(property);
        }
        items = property.FindPropertyRelative("Items");
        Color colorOld;
        EditorGUILayout.BeginHorizontal();
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(200), GUILayout.Height((_buttonHeight + 2) * items.arraySize + _buttonHeight * 2));
            {
                for (int i = 0; i < items.arraySize; i++)
                {
                    SerializedProperty serializedProperty = items.GetArrayElementAtIndex(i);
                    EditorGUILayout.BeginHorizontal();
                    {
                        colorOld = GUI.backgroundColor;
                        if (serializedProperty.objectReferenceValue == null)
                        {
                            GUI.backgroundColor = Color.red;
                            if (GUILayout.Button("Class not found!", GUILayout.Height(_buttonHeight)))
                            {
                            }
                            if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(_buttonHeight)))
                            {
                                items.DeleteArrayElementAtIndex(i);
                            }
                            GUI.backgroundColor = colorOld;
                        }
                        else
                        {
                            bool selected = selectedIndex == i;
                            if (selected)
                            {
                                GUI.backgroundColor = Reflection.SerializedPropertyGetPropertyValue<Color>(serializedProperty.objectReferenceValue, "ButtonColorSelected");
                            }
                            else
                            {
                                GUI.backgroundColor = Reflection.SerializedPropertyGetPropertyValue<Color>(serializedProperty.objectReferenceValue, "ButtonColorDefault");
                            }
                            if (GUILayout.Button(Reflection.SerializedPropertyGetPropertyValue<String>(serializedProperty.objectReferenceValue, "DisplayName"), GUILayout.Height(_buttonHeight)))
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

                            if (_allowUserToRemoveItems)
                            {
                                GUI.backgroundColor = Color.red;
                                if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(_buttonHeight)))
                                {
                                    items.DeleteArrayElementAtIndex(i);
                                    items.DeleteArrayElementAtIndex(i);
                                    selectedProperty = null;
                                    selectedIndex = -1;
                                }
                            }

                            GUI.backgroundColor = colorOld;
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }

                if (_allowUserToAddItems)
                {
                    colorOld = GUI.backgroundColor;
                    GUI.backgroundColor = Color.green;
                    if (GUILayout.Button("Add " + ItemsType.Name, GUILayout.Height(_buttonHeight)))
                    {
                        UpdateItems(property);
                        itemSelectPopupToShow = itemSelectPopup;
                    }
                    GUI.backgroundColor = colorOld;
                    if (itemSelectPopupToShow != null)
                    {
                        itemSelectPopupToShow.ShowAsContext();
                        itemSelectPopupToShow = null;
                    }
                }

            }
            EditorGUILayout.EndScrollView();

            EditorGUILayout.BeginVertical();
            {
                if (selectedProperty != null)
                {
                    EditorGUI.PropertyField(position, selectedProperty, true);
                }
            }
            EditorGUILayout.EndVertical();

        }
        EditorGUILayout.EndHorizontal();

    }

    //http://answers.unity.com/comments/1495747/view.html
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return -4f; // this seems to match closest to non-property drawer version
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

    private void UpdateGraphicSettings(SerializedProperty property)
    {
        _buttonHeight = Reflection.SerializedPropertyGetPropertyValue<int>(property, "ButtonHeight");
        _allowUserToAddItems = Reflection.SerializedPropertyGetPropertyValue<bool>(property, "AllowUserToAddItems");
        _allowUserToRemoveItems = Reflection.SerializedPropertyGetPropertyValue<bool>(property, "AllowUserToRemoveItems");
    }

}
