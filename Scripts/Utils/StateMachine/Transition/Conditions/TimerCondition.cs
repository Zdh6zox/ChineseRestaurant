using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCondition : ConditionBase
{
    public float _timerDuration;
    private float _timePassed;

    public Expression _expression;

    public void SetTimeDuration(float duration)
    {
        _timerDuration = duration;
        _timePassed = 0;
    }

    public override void ResetCondition(IStateMachineOwner owner)
    {
        base.ResetCondition(owner);
        _timePassed = 0;
    }

    protected override void UpdateIsVerfied(IStateMachineOwner owner)
    {
        _timePassed += Time.deltaTime;
        if (_timePassed >= _timerDuration)
        {
            _isVerfied = true;
        }
        else
        {
            _isVerfied = false;
        }
    }
}
