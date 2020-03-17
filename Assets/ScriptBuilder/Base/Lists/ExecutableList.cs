using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExecutableList : DrawableScriptableObjectList<Executable>
{
    public override string DisplayName
    {
        get
        {
            return "Executable";
        }
    }

}
