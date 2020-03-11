using UnityEngine;
using System.Collections;
using System;

//IDK why but this class is not allowed to inherit UnityEngine.Object or ScriptablObject
[System.Serializable]
public class DrawableScriptableObjectList<T> where T : DrawableScriptableObject
{

    [SerializeField]
    public T[] Items;

    public virtual string DisplayName
    {
        get {
            return typeof(T).Name;
        }
    }

    public virtual Type ObjectType
    {
        get
        {
            return typeof(T);
        }
    }

}
