using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager
{
    private GameManager m_GameManager;

    public void Initialize(GameManager gameManager)
    {
        m_GameManager = gameManager;
    }

    public void CreateTask(ITaskAssignee assignee)
    {

    }
}
