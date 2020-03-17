using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Event : DrawableScriptableObject
{

    public ExecutableList ExecuteOnInvoke = new ExecutableList();

    public void Invoke()
    {
        if (ExecuteOnInvoke == null || ExecuteOnInvoke.Items == null) {
            return;
        }
        foreach (Executable executable in ExecuteOnInvoke.Items)
        {
            try
            {
                executable.Invoke();
                executable.ThrownException = "";
            }
            catch (Exception ex) {
                executable.ThrownException = ex.Message;
                Debug.LogException(ex);
            }
        }
    }

    public override string Prefix
    {
        get
        {
            return "Event";
        }
    }

}

