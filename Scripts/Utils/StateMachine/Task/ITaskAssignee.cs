using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITaskAssignee
{
    void ReceiveTask(TaskBase actorTask);
}
