using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallWaiterState : StateBase
{
    public CallWaiterState()
    {
        this.StateName = "Call Waiter State";
    }

    public override void OnEnterState(IStateMachineOwner owner)
    {
        if (owner.GetType() != typeof(Customer))
        {
            Object ownerObject = owner as Object;
            Debug.LogError(string.Format("{0} is not customer, cannot use EnterInnState", ownerObject.name));
            throw new System.Exception("Wrong State Usage");
        }

        Customer customer = owner as Customer;
        Debug.Log(string.Format("{0} Enter CallWaiterState", customer.Data.CharacterName));
    }

    public override void OnExitState(IStateMachineOwner owner)
    {
        Customer customer = owner as Customer;
        Debug.Log(string.Format("{0} Exit CallWaiterState", customer.Data.CharacterName));
    }

    public override void OnUpdateState(IStateMachineOwner owner)
    {
        Customer customer = owner as Customer;
        //Debug.Log(string.Format("{0} Update CallWaiterState", customer.Data.CharacterName));
    }
}



