using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExecutableDestroyObject : Executable
{
    public GameObject GameObject;
    public float Delay = 0.0f;

    public override void Invoke()
    {
        GameObject.Destroy(GameObject, Delay);
    }

    public override string Folder {
        get{

            return "GameObject";
        }
    }
    
}
