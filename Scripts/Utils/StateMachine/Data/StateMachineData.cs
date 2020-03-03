using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class StateMachineData : ScriptableObject
{
    public List<StateDataBase> States = new List<StateDataBase>();

    [ContextMenuItem("Browse Existing State", "ShowInitStateWindow")]
    public StateDataBase InitialState;

    public void CreateStateData(System.Type type)
    {
        bool isExisting = false;
        foreach (StateDataBase state in States)
        {
            if (state.GetType() == type)
            {
                isExisting = true;
                break;
            }
        }
        if (!isExisting)
        {
            StateDataBase newState = ScriptableObject.CreateInstance(type) as StateDataBase;
            if (newState != null)
            {
                States.Add(newState);
                string path = AssetDatabase.GetAssetPath(this);
                AssetDatabase.AddObjectToAsset(newState, path);
                AssetDatabase.ImportAsset(path);
            }
        }
    }

    [ContextMenu("Create New State")]
    public void ShowStateBrowser()
    {
        TypeBrowser currentBrowser = TypeBrowser.ShowBrowser();
        currentBrowser.CreateNewFunc += this.CreateStateData;
        currentBrowser.InitBrowser(typeof(StateDataBase));
    }

    public void ShowInitStateWindow()
    {
        InitStateWindow currentBrowser = InitStateWindow.ShowWindow();
        currentBrowser.SetOwner(this);
    }
}