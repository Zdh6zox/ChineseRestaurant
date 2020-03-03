using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class StateBase
{
    [ReadOnly]
    public string StateName;

    public abstract void OnEnterState(IStateMachineOwner owner);

    public abstract void OnExitState(IStateMachineOwner owner);

    public abstract void OnUpdateState(IStateMachineOwner owner);

    public List<TransitionBase> Transitions = new List<TransitionBase>();
}



