using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalStateTask : TaskBase
{
    private StateBase _externalState;
    ExternalStateTask(string taskName, StateBase externalState)
    {
        this.TaskName = taskName;
        _externalState = externalState;
    }

    public override void UnwrapTask()
    {
        ITaskAssignee assignee = this.TaskAssignee;

        IStateMachineOwner sm_Owner = assignee as IStateMachineOwner;
        if(sm_Owner != null)
        {
            StateMachine sm = sm_Owner.GetStateMachine();
            sm.TransitToExternalState(_externalState);
        }
    }
}
