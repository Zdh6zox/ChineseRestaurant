using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TransitionBase
{
    [ReadOnly]
    public string TransitionName;

    public StateBase OwnerState;

    public abstract bool IsValid(IStateMachineOwner owner);

    public abstract void OnTransition(IStateMachineOwner owner);

    public StateBase NextState;

}
