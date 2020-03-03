using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConditionBase : IExpressionUnit
{
    protected bool _isVerfied = false;

    protected abstract void UpdateIsVerfied(IStateMachineOwner owner);

    public bool GetIsVerfied(IStateMachineOwner owner)
    {
        UpdateIsVerfied(owner);
        return _isVerfied;
    }

    public virtual void ResetCondition(IStateMachineOwner owner) { _isVerfied = false; }

    public bool GetValue(IStateMachineOwner owner)
    {
        return GetIsVerfied(owner);
    }

    public string GetName(IStateMachineOwner owner)
    {
        throw new System.NotImplementedException();
    }

    public void Reset(IStateMachineOwner owner)
    {
        ResetCondition(owner);
    }
}
