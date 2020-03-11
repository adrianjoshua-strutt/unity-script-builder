using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAwake : Event
{

    void Awake() {
        if (Application.isPlaying)
            this.Invoke();
    }

    public override string Folder {
        get {
            return "Misc";
        }
    }

}
