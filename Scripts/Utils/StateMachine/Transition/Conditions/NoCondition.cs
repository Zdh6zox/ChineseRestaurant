using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoCondition : ConditionBase
{
    public new string GetName(IStateMachineOwner owner)
    {
        return "true";
    }

    protected override void UpdateIsVerfied(IStateMachineOwner owner)
    {
        _isVerfied = true;
    }
}
