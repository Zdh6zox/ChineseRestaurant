using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PayBillState : StateBase
{
    public PayBillState()
    {
        this.StateName = "Pay Bill State";
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
        Debug.Log(string.Format("{0} Enter PayBillState", customer.Data.CharacterName));


    }

    public override void OnExitState(IStateMachineOwner owner)
    {
        Customer customer = owner as Customer;
        Debug.Log(string.Format("{0} Exit PayBillState", customer.Data.CharacterName));

        Blackboard customer_bb = customer.GetComponent<Blackboard>();

        if(customer_bb)
        {
            if(customer_bb.GetBBValue<Table>("Table",out Table table))
            {
                table.OnCustomerLeave();
            }
        }
    }

    public override void OnUpdateState(IStateMachineOwner owner)
    {

    }
}


