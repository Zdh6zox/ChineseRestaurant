using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressionTransition : TransitionBase
{
    public ExpressionTransition()
    {
        this.TransitionName = "Expression Transition";
    }

    public Expression _Expression;

    public void SetExpression(Expression expression)
    {
        _Expression = expression;
    }

    public override bool IsValid(IStateMachineOwner owner)
    {
        return _Expression.GetExpressionResult(owner);
    }

    public override void OnTransition(IStateMachineOwner owner)
    {
        _Expression.Reset(owner);
    }
}


