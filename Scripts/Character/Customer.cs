using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CustomerType : uint
{
    CustomerType_Beggar, //乞丐
    CustomerType_Peasant, //农民
    CustomerType_Citizen, //市民
    CustomerType_Councillor, //员外
    CustomerType_Swordman, //侠客
    CustomerType_Escort, //镖师
    CustomerType_Merchant, //商人
    CustomerType_Hooligan, //流氓
    CustomerType_Mafia,   //地头蛇

    //新的Type加在上面
    CustomerType_Count
}

//对于Customer类，初步想法是先定义特殊的Customer，然后在初次运行时生成Customer表，储存在本地
//在之后的游戏过程中都是读取该表来生成Customer
[Serializable]
public class CustomerData : CharacterData
{
    [SerializeField]
    private CustomerType m_Type;
    [SerializeField]
    private List<Recipe> m_PreferRecipe = new List<Recipe>();
    [SerializeField]
    private float m_RestaurantRate;

    //顾客类型
    public CustomerType Type { get => m_Type; set => m_Type = value; }
    //偏爱的菜
    public List<Recipe> PreferRecipe { get => m_PreferRecipe; set => m_PreferRecipe = value; }
    public float RestaurantRate { get => m_RestaurantRate; set => m_RestaurantRate = value; }

    public static CustomerData GenerateRandomData()
    {
        UnityEngine.Random.Range(0, 5);
        return new CustomerData();
    }
}

public class Customer : MonoBehaviour , ITaskAssignee , IStateMachineOwner
{
    public CustomerData Data = new CustomerData();
    //顾客心情指数，在Spawn的时候指定，与顾客的客栈评分有关，并在Unspawn反应到客栈评分。
    public float CurrentMoodScore = 0.0f;
    private NavMeshAgent agent;
    public StateMachine _StateMachine;
    private bool _IsLeader = false;

    public StateBase _test;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InitializeStateMachine();
        _StateMachine.Start();
    }

    private void Update()
    {
        _StateMachine.Update();
    }

    public void InitializeCustomer()
    {
        //指定初始心情指数
    }

    public bool MoveToLocation(Vector3 loc)
    {
        Blackboard bb = GetComponent<Blackboard>();
        if(bb)
        {
            agent.SetDestination(loc);
            bb.AddOrModifyBBValue<Vector3>("NavTargetPos", loc);
            return true;
        }

        return false;
    }

    public void StopMoving()
    {
        agent.isStopped = true;
    }

    public void ReceiveTask(TaskBase actorTask)
    {
    }

    private void InitializeStateMachine()
    {
        _StateMachine = StateMachineFactory.CreateStateMachine(this);
    }

    public StateMachine GetStateMachine()
    {
        return _StateMachine;
    }

    public void SetStateMachine(StateMachine stateMachine)
    {
        _StateMachine = stateMachine;
    }

    public void SetAsLeader()
    {
        _IsLeader = true;
    }

    public bool GetIsLeader()
    {
        return _IsLeader;
    }

    public void LeaveInn()
    {
        Utils.GetGameManager().GetCustomerManager().OnCustomerUnSpawned(this);
        Destroy(gameObject);
    }
}

[System.Serializable]
public class CustomerGroup
{
    public List<Customer> _Members = new List<Customer>();
    public Customer _Leader;

    private CustomerGroup() { }

    public static CustomerGroup CreateGroup(Customer leader,List<Customer> members)
    {
        CustomerGroup newGroup = new CustomerGroup();
        newGroup._Leader = leader;
        newGroup._Members = members;
        return newGroup;
    }

    public static CustomerGroup CreateGroup(Customer leader)
    {
        CustomerGroup newGroup = new CustomerGroup();
        newGroup._Leader = leader;
        return newGroup;
    }

    public static CustomerGroup GetGroupViaLeader(Customer leader)
    {
        CustomerGroup group = CreateGroup(leader);
        Blackboard bb = leader.GetComponent<Blackboard>();

        if (bb)
        {
            bb.GetBBValue<CustomerGroup>("Group", out group);
        }
        return group;
    }

    public void ExpandGroup(List<Customer> members)
    {
        _Members.AddRange(members);
    }

    public int GetGroupSize()
    {
        return _Members.Count + 1;
    }
}

