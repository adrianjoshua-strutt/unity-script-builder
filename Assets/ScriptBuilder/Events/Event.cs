using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventOnEnable : Event
{

    void OnEnable()
    {
        if (Application.isPlaying)
            this.Invoke();
    }

    public override string Folder
    {
        get
        {
            return "Misc";
        }
    }

}
