using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class StateMachine
{
    public List<StateBase> States = new List<StateBase>();

    public StateBase InitialState;

    [ReadOnly]
    public StateBase ActiveState;

    [ReadOnly]
    public StateBase HistoryState;

    public IStateMachineOwner Owner;

    public void Start()
    {
        if (InitialState != null)
        {
            InitialState.OnEnterState(Owner);
            ActiveState = InitialState;
        }

        #region Test
        //Test
        //Expression test = new Expression();
        //test.SetOwner(Owner);
        //NoCondition con1 = new NoCondition();
        //NoCondition con2 = new NoCondition();
        //NoCondition con3 = new NoCondition();
        //NoCondition con4 = new NoCondition();
        //NoCondition con5 = new NoCondition();
        //NoCondition con6 = new NoCondition();
        //////Test true and (!true and (true or !true) or (!true or true))
        ////test.PushUnit(con1);
        ////test.PushOperator(new AndOperator());
        ////test.PushOperator(new SeparatorStartOperator());
        ////test.PushOperator(new NotOperator());
        ////test.PushUnit(con2);
        ////test.PushOperator(new AndOperator());
        ////test.PushOperator(new SeparatorStartOperator());
        ////test.PushUnit(con3);
        ////test.PushOperator(new OrOperator());
        ////test.PushOperator(new NotOperator());
        ////test.PushUnit(con4);
        ////test.PushOperator(new SeparatorEndOperator());
        ////test.PushOperator(new OrOperator());
        ////test.PushOperator(new SeparatorStartOperator());
        ////test.PushOperator(new NotOperator());
        ////test.PushUnit(con5);
        ////test.PushOperator(new OrOperator());
        ////test.PushUnit(con6);
        ////test.PushOperator(new SeparatorEndOperator());
        ////test.PushOperator(new SeparatorEndOperator());

        ////Test !true or (true and !!true)
        //test.PushOperator(new NotOperator());
        //test.PushUnit(con1);
        //test.PushOperator(new OrOperator());
        //test.PushOperator(new SeparatorStartOperator());
        //test.PushUnit(con2);
        //test.PushOperator(new AndOperator());
        //test.PushOperator(new NotOperator());
        //test.PushOperator(new NotOperator());
        //test.PushUnit(con3);
        //test.PushOperator(new SeparatorEndOperator());

        //bool value = test.GetExpressionResult(Owner);

        //if(value)
        //{
        //    Debug.Log("True!!!");
        //}
        //else
        //{
        //    Debug.Log("False!!!");
        //}
        #endregion
    }

    public void Update()
    {
        List<TransitionBase> transitions = ActiveState.Transitions;
        foreach(TransitionBase transition in transitions)
        {
            if(transition.IsValid(Owner))
            {
                ActiveState.OnExitState(Owner);
                ActiveState = transition.NextState;
                transition.OnTransition(Owner);
                if(ActiveState != null)
                {
                    ActiveState.OnEnterState(Owner);
                }
                return;
            }
        }

        ActiveState.OnUpdateState(Owner);
    }

    public void TransitToExternalState(StateBase externalState)
    {
        //ignore transition condition
        HistoryState = ActiveState;
        ActiveState = externalState;

        List<TransitionBase> transitions = ActiveState.Transitions;
        foreach(TransitionBase transition in transitions)
        {
            transition.NextState = HistoryState;
        }
    }
}


public class CreateStateMachineAsset : UnityEditor.Editor
{
    StateMachine m_SM;
    public static readonly string SaveDir = "Assets/Prefabs/StateMachine";
    [MenuItem("StateMachine/CreateStateMachine")]
    static void Create()
    {
        ScriptableObject m_SM = ScriptableObject.CreateInstance<StateMachineData>();
        if (!Directory.Exists(SaveDir))
        {
            Directory.CreateDirectory(SaveDir);
        }

        string savePath = string.Format("{0}/{1}.asset", SaveDir, "NewStateMachine");

        AssetDatabase.CreateAsset(m_SM, savePath);
    }
}
