using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GotoSeatsState : StateBase
{
    public GotoSeatsState()
    {
        this.StateName = "Go To Seats State";
    }

    private bool _destinationSettled = false;

    public override void OnEnterState(IStateMachineOwner owner)
    {
        if (owner.GetType() != typeof(Customer))
        {
            Object ownerObject = owner as Object;
            Debug.LogError(string.Format("{0} is not customer, cannot use GotoSeatsState", ownerObject.name));
            throw new System.Exception("Wrong State Usage");
        }

        Customer customer = owner as Customer;
        Debug.Log(string.Format("{0} Enter GotoSeatsState", customer.Data.CharacterName));
        Utils.GetGameManager().GetUIManager().PopoutNewMessage(customer.gameObject, "Enter GotoSeatsState");

        if(customer.GetIsLeader())
        {
            InnManager innManager = Utils.GetGameManager().GetInnManager();

            Table table;
            if(innManager.QueryTable(customer,out table))
            {
                Vector3 des = table.PopOutSeats(customer);

                Blackboard leader_bb = customer.GetComponent<Blackboard>();
                leader_bb.AddOrModifyBBValue<Table>("Table", table);

                CustomerGroup group = CustomerGroup.GetGroupViaLeader(customer);

                foreach (Customer member in group._Members)
                {
                    Blackboard member_bb = member.GetComponent<Blackboard>();
                    member_bb.AddOrModifyBBValue<Table>("Table", table);
                }

                customer.MoveToLocation(des);
                _destinationSettled = true;
            }
        }
        else
        {
            //non-leader should follow leader decisions.
        }
    }

    public override void OnExitState(IStateMachineOwner owner)
    {
        Customer customer = owner as Customer;
        Debug.Log(string.Format("{0} Exit GotoSeatsState", customer.Data.CharacterName));

        //customer.StopMoving();
    }

    public override void OnUpdateState(IStateMachineOwner owner)
    {
        Customer customer = owner as Customer;
        //Debug.Log(string.Format("{0} Update GotoSeatsState", customer.Data.CharacterName));

        if(!customer.GetIsLeader()&&!_destinationSettled)
        {
            Table targetTable;
            Blackboard customer_bb = customer.GetComponent<Blackboard>();
            if(customer_bb)
            {
                if(customer_bb.GetBBValue<Table>("Table",out targetTable))
                {
                    Vector3 des = targetTable.PopOutSeats(customer);
                    customer.MoveToLocation(des);
                    _destinationSettled = true;
                }
            }
        }
    }
}

