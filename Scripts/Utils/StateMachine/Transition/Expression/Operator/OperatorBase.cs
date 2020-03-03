using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OperatorBase
{
    public abstract bool NeedLeftUnit();

    public abstract bool NeedRightUnit();

    public abstract void SetLeftUnit(IExpressionUnit unit);

    public abstract void SetRightUnit(IExpressionUnit unit);

    public abstract ExpressionIntermediate GetOperatorResult();

    public abstract void DebugText(IStateMachineOwner owner);
}

public class ExpressionIntermediate : IExpressionUnit
{
    public delegate bool GetValueDelegate(IStateMachineOwner owner);
    public delegate void ResetDelegate(IStateMachineOwner owner);

    public GetValueDelegate GetValueFunc;
    public ResetDelegate ResetFunc;

    public bool GetValue(IStateMachineOwner owner)
    {
        return GetValueFunc(owner);
    }

    public string GetName(IStateMachineOwner owner)
    {
        return GetValueFunc(owner).ToString();
    }

    public void Reset(IStateMachineOwner owner)
    {
        ResetFunc(owner);
    }
}
