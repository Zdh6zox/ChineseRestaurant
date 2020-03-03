using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class TaskBase
{
    public string TaskName;
    public ITaskAssignee TaskAssignee;

    public abstract void UnwrapTask();
}
