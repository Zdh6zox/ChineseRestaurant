using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TransitionDataBase : ScriptableObject
{
    public StateDataBase OwnerState;

    public StateDataBase NextState;

    public virtual string GetClassName()
    {
        throw new System.NotImplementedException();
    }

    
}

