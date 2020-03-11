using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventList : DrawableScriptableObjectList<Event>
{

    public override string DisplayName{
        get {
            return "Events";
        }
    }

}
