using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExitInnState : StateBase
{
    public ExitInnState()
    {
        this.StateName = "Exit Inn State";
    }

    public override void OnEnterState(IStateMachineOwner owner)
    {
        if (owner.GetType() != typeof(Customer))
        {
            Object ownerObject = owner as Object;
            Debug.LogError(string.Format("{0} is not customer, cannot use ExitInnState", ownerObject.name));
            throw new System.Exception("Wrong State Usage");
        }

        Customer customer = owner as Customer;
        Debug.Log(string.Format("{0} Enter ExitInnState", customer.Data.CharacterName));

        InnManager inn = Utils.GetGameManager().GetInnManager();
        customer.MoveToLocation(inn.GetExitLocation());
    }

    public override void OnExitState(IStateMachineOwner owner)
    {
        Customer customer = owner as Customer;
        Debug.Log(string.Format("{0} Exit ExitInnState", customer.Data.CharacterName));

        customer.LeaveInn();
    }

    public override void OnUpdateState(IStateMachineOwner owner)
    {
        Customer customer = owner as Customer;
        //Debug.Log(string.Format("{0} Update ExitInnState", customer.Data.CharacterName));
    }
}


