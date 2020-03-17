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
    public virtual string DisplayName
    {
        get
        {
            return StringHelper.SplitCamelCase(Name);
        }
    }

    private Color _buttonColorDefault = new Color(1, 1, 1);
    public virtual Color ButtonColorDefault
    {
        get
        {
            return _buttonColorDefault;
        }
    }

    private Color _buttonColorSelected = Color.gray;
    public virtual Color ButtonColorSelected
    {
        get
        {
            return _buttonColorSelected;
        }
    }


}
