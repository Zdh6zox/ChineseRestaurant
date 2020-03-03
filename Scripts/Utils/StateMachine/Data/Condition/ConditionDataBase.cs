using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConditionDataBase : ScriptableObject
{
    public virtual string GetClassName()
    {
        throw new System.NotImplementedException();
    }
}
