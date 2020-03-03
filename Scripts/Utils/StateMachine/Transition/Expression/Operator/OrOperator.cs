using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrOperator : OperatorBase
{
    public IExpressionUnit Left;
    public IExpressionUnit Right;

    public override void DebugText(IStateMachineOwner owner)
    {
        string debugStr;
        debugStr = Left.GetName(owner) + " Or " + Right.GetName(owner);

        Debug.Log(debugStr);
    }

    public override ExpressionIntermediate GetOperatorResult()
    {
        ExpressionIntermediate intermediate = new ExpressionIntermediate();
        intermediate.GetValueFunc = (IStateMachineOwner x) => { return Left.GetValue(x) || Right.GetValue(x); };

        return intermediate;
    }

    public override bool NeedLeftUnit()
    {
        return true;
    }

    public override bool NeedRightUnit()
    {
        return true;
    }

    public override void SetLeftUnit(IExpressionUnit unit)
    {
        Left = unit;
    }

    public override void SetRightUnit(IExpressionUnit unit)
    {
        Right = unit;
    }
}
