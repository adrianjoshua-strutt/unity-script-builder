using UnityEngine;
using UnityEditor;

public class DrawableScriptableObjectException : DrawableScriptableObject
{

    private bool _hasException;
    public bool HasException
    {
        get
        {
            return _hasException;
        }
    }

    private string _thrownException;
    public string ThrownException
    {
        get
        {
            return _thrownException;
        }
        set
        {
            _thrownException = value;
            _hasException = ThrownException != null && ThrownException.Length > 0;
        }
    }

    private Color _colorException = Color.red * 0.8f;

    public override Color ButtonColorDefault
    {
        get
        {
            if (HasException)
            {
                return base.ButtonColorDefault * _colorException;
            }
            else
            {
                return base.ButtonColorDefault;
            }
        }
    }

    public override Color ButtonColorSelected
    {
        get
        {
            if (HasException)
            {
                return base.ButtonColorSelected * _colorException;
            }
            else
            {
                return base.ButtonColorSelected;
            }
        }
    }

    public override string DisplayName
    {
        get
        {
            if (HasException)
            {
                return "Error: " + base.DisplayName;
            }
            else
            {
                return base.DisplayName;
            }
            
        }
    }

}