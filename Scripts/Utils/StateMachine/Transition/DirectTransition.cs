using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectTransition : TransitionBase
{
    public DirectTransition()
    {
        this.TransitionName = "Direct Transition";
    }

    public override bool IsValid(IStateMachineOwner owner)
    {
        return true;
    }

    public override void OnTransition(IStateMachineOwner owner)
    {

    }
}



