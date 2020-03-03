using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaitingPeopleState : StateBase
{
    public WaitingPeopleState()
    {
        this.StateName = "Waiting People State";
    }

    public override void OnEnterState(IStateMachineOwner owner)
    {
        throw new System.NotImplementedException();
    }

    public override void OnExitState(IStateMachineOwner owner)
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdateState(IStateMachineOwner owner)
    {
        throw new System.NotImplementedException();
    }
}

