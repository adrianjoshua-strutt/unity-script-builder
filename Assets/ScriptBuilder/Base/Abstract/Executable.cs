using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Executable : DrawableScriptableObjectException
{
    public abstract void Invoke();

    public override string Prefix {
        get {
            return "Executable";
        }
    }

}
