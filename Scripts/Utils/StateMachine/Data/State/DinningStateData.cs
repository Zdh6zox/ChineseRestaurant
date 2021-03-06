﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinningStateData : StateDataBase
{
    public void OnEnable()
    {
        name = GetClassName();
    }

    public override string GetClassName()
    {
        string className = GetType().Name;
        className.Replace("Data", "");
        return className;
    }
}