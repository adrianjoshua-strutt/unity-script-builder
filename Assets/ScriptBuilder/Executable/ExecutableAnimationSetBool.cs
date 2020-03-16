using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutableAnimationSetBool : Executable
{

    public Animator Animator;

    public string AnimationName;
    public bool Value;

    public override void Invoke()
    {
        Animator.SetBool(AnimationName, Value);
    }

    public override string Folder
    {
        get
        {
            return "Animation";

        }
    }

}
