using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExpressionUnit
{
    bool GetValue(IStateMachineOwner owner);

    string GetName(IStateMachineOwner owner);

    void Reset(IStateMachineOwner owner);
}
