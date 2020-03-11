using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Event : ScriptableObject
{

    public ExecutableList ExecuteOnInvoke = new ExecutableList();

    public void Invoke()
    {
        if (ExecuteOnInvoke == null || ExecuteOnInvoke.Executables == null) {
            return;
        }
        foreach (Executable executable in ExecuteOnInvoke.Executables)
        {
            executable.Invoke();
        }
    }

    public virtual string getFolder()
    {
        return "miscellaneous";
    }

    public virtual string getName()
    {
        string name = this.GetType().Name;
        string prefix = "Event";
        if (name.StartsWith(prefix))
        {
            name = name.Substring(prefix.Length);
        }
        return name;
    }

    public virtual string getFullName()
    {
        return getFolder() + "/" + getName();
    }


}

