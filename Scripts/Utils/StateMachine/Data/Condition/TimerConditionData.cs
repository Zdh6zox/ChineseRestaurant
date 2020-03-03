using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerConditionData : ConditionDataBase
{
    public override string GetClassName()
    {
        string className = GetType().Name;
        className.Replace("Data", "");
        return className;
    }
}
