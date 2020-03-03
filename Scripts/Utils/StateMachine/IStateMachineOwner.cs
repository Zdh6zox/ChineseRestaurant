using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateMachineOwner
{
    void SetStateMachine(StateMachine stateMachine);
    StateMachine GetStateMachine();
}
