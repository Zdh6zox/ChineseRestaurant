using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : MonoBehaviour, ITaskAssignee, IStateMachineOwner
{
    public StateMachine GetStateMachine()
    {
        throw new System.NotImplementedException();
    }

    public void ReceiveTask(TaskBase actorTask)
    {
        throw new System.NotImplementedException();
    }

    public void SetStateMachine(StateMachine stateMachine)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class WaiterRequest
{
    public enum WaiterRequestType
    {
        Discount,
        PostponeDish,
        CancelDish,
        AddDish,
        FindTable,
        PayBill,
        Complain,
        Compliment
    }

    public WaiterRequestType _Type;
}

