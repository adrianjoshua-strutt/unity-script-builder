using UnityEngine;
using System.Collections;

public class DrawableScriptableObject : ScriptableObject
{
    public virtual string Prefix
    {
        get
        {
            return "";
        }
    }

    public virtual string Name
    {
        get
        {
            string name = this.GetType().Name;
            if (name.ToLower().StartsWith(Prefix.ToLower()))
            {
                name = name.Substring(Prefix.Length);
            }
            return name;
        }
    }

    public virtual string Folder
    {
        get
        {
            return "";
        }
    }

    public virtual string FullName
    {
        get
        {
            if (Folder.Length > 0)
            {
                return Folder + "/" + Name;
            }
            else {
                return Name;
            }
        }
    }

}
