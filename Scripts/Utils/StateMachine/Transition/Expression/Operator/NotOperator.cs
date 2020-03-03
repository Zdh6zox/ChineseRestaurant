using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotOperator : OperatorBase
{
    public IExpressionUnit Right;

    public override void DebugText(IStateMachineOwner owner)
    {
        string debugStr;
        debugStr = "!" + Right.GetName(owner);

        Debug.Log(debugStr);
    }

    public override ExpressionIntermediate GetOperatorResult()
    {
        ExpressionIntermediate intermediate = new ExpressionIntermediate();
        intermediate.GetValueFunc = (IStateMachineOwner x) => { return !Right.GetValue(x); };

        return intermediate;
    }

    public override bool NeedLeftUnit()
    {
        return false;
    }

    public override bool NeedRightUnit()
    {
        return true;
    }

    public override void SetLeftUnit(IExpressionUnit unit)
    {
        throw new System.NotImplementedException();
    }

    public override void SetRightUnit(IExpressionUnit unit)
    {
        Right = unit;
    }
}
