using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Executable : ScriptableObject
{
    public abstract void Invoke();

    public virtual string getFolder() {
        return "miscellaneous";
    }

    public virtual string getName()
    {
        string name = this.GetType().Name;
        string prefix = "Executable";
        if (name.StartsWith(prefix)) {
            name = name.Substring(prefix.Length);
        }
        return name;
    }

    public virtual string getFullName() {
        return getFolder() + "/" + getName();
    }

}
