using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OrderDishesState : StateBase
{
    public OrderDishesState()
    {
        this.StateName = "Order Dishes State";
    }

    public override void OnEnterState(IStateMachineOwner owner)
    {
        if (owner.GetType() != typeof(Customer))
        {
            Object ownerObject = owner as Object;
            Debug.LogError(string.Format("{0} is not customer, cannot use OrderDishesState", ownerObject.name));
            throw new System.Exception("Wrong State Usage");
        }

        Customer customer = owner as Customer;
        Debug.Log(string.Format("{0} Enter OrderDishesState", customer.Data.CharacterName));
    }

    public override void OnExitState(IStateMachineOwner owner)
    {
        Customer customer = owner as Customer;
        Debug.Log(string.Format("{0} Exit OrderDishesState", customer.Data.CharacterName));
    }

    public override void OnUpdateState(IStateMachineOwner owner)
    {
        Customer customer = owner as Customer;
    }
}


