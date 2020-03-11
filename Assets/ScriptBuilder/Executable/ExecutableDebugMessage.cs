using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExecutableDebugMessage : Executable
{
    public string Message;

    public override void Invoke() {
        Debug.Log(Message);       
    }

}
