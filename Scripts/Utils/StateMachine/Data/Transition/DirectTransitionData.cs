using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectTransitionData : TransitionDataBase
{
    public void OnEnable()
    {
        name = GetType().Name;
    }

    public override string GetClassName()
    {
        string className = GetType().Name;
        className.Replace("Data", "");
        return className;
    }
}
