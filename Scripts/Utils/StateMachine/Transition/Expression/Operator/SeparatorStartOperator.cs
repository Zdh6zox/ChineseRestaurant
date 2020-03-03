using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//对于括号来说，正常顺序下不能有括号
//比如： a && (b || c && d) correct
//       a && (b || (c || d) correct
//       a && ((b || c) && d) incorrect
public class SeparatorStartOperator : OperatorBase
{
    public IExpressionUnit Right;

    public override void DebugText(IStateMachineOwner owner)
    {
        string debugStr;
        debugStr = "(" + Right.GetName(owner);

        Debug.Log(debugStr);
    }

    public override ExpressionIntermediate GetOperatorResult()
    {
        throw new System.NotImplementedException();
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
