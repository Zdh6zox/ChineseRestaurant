using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//StateMachine工厂类
public class StateMachineFactory
{
    public static StateMachine CreateStateMachine(IStateMachineOwner smOwner)
    {
        StateMachine newSM = new StateMachine();
        newSM.Owner = smOwner;
        //TODO:
        //使用可在Editor内编辑的StateMachineInfo类来创建StateMachine，当前暂时使用hardcode

        if(smOwner.GetType() == typeof(Customer))
        {
            //创建EnterInnState - State itself
            EnterInnState enterInn = new EnterInnState();
            newSM.InitialState = enterInn;
            newSM.States.Add(enterInn);

            //创建GotoSeatsState
            GotoSeatsState gotoSeats = new GotoSeatsState();
            newSM.States.Add(gotoSeats);

            //创建PayBillState
            PayBillState payBill = new PayBillState();
            newSM.States.Add(payBill);

            //创建ExitInnState
            ExitInnState exitInn = new ExitInnState();
            newSM.States.Add(exitInn);



            //创建Transition
            List<TransitionBase> enterInnTransitions = new List<TransitionBase>();
            ExpressionTransition enterInnSCTransit = new ExpressionTransition();
            enterInnSCTransit.NextState = gotoSeats;
            TimerCondition _2sTimerCondition = new TimerCondition();
            _2sTimerCondition.SetTimeDuration(2);
            Expression enterInnSCTransitExp = new Expression();
            enterInnSCTransitExp.PushUnit(_2sTimerCondition);
            enterInnSCTransit.SetExpression(enterInnSCTransitExp);
            enterInnTransitions.Add(enterInnSCTransit);
            enterInn.Transitions = enterInnTransitions;

            List<TransitionBase> gotoSeatsTransitions = new List<TransitionBase>();
            ExpressionTransition gotoSeatsSCTransit = new ExpressionTransition();
            gotoSeatsSCTransit.NextState = payBill;
            IsReachDestination isReachDestination = new IsReachDestination();
            isReachDestination.SetDistanceThreshold(0.2f);
            Expression gotoSeatsSCTransitExp = new Expression();
            gotoSeatsSCTransitExp.PushUnit(isReachDestination);
            gotoSeatsSCTransit.SetExpression(gotoSeatsSCTransitExp);
            gotoSeatsTransitions.Add(gotoSeatsSCTransit);
            gotoSeats.Transitions = gotoSeatsTransitions;

            List<TransitionBase> payBillTransitions = new List<TransitionBase>();
            ExpressionTransition payBillSCTransit = new ExpressionTransition();
            Expression payBillSCTransitExp = new Expression();
            payBillSCTransitExp.PushUnit(new NoCondition());
            payBillSCTransit.NextState = exitInn;
            payBillSCTransit.SetExpression(payBillSCTransitExp);
            payBillTransitions.Add(payBillSCTransit);
            payBill.Transitions = payBillTransitions;

            List<TransitionBase> exitInnTransitions = new List<TransitionBase>();
            ExpressionTransition exitInnSCTransit = new ExpressionTransition();
            Expression exitInnSCTransitExp = new Expression();
            exitInnSCTransitExp.PushUnit(isReachDestination);
            exitInnSCTransit.SetExpression(exitInnSCTransitExp);
            exitInnTransitions.Add(exitInnSCTransit);
            exitInn.Transitions = exitInnTransitions;
        }

        return newSM;
    }

}
