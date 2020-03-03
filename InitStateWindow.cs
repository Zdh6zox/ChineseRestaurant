using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InitStateWindow : EditorWindow
{
    StateMachineData _currentOperatingSM;

    InitStateWindow()
    {
        this.titleContent = new GUIContent("Existing State");
    }

    public static InitStateWindow ShowWindow()
    {
        return EditorWindow.GetWindow(typeof(InitStateWindow)) as InitStateWindow;
    }

    public void SetOwner(StateMachineData sm)
    {
        _currentOperatingSM = sm;
    }

    public void OnGUI()
    {
        if(_currentOperatingSM != null)
        {
            foreach(StateDataBase state in _currentOperatingSM.States)
            {
                if(GUILayout.Button(state.GetClassName()))
                {
                    _currentOperatingSM.InitialState = state;
                    Close();
                }
            }
        }
    }
}
