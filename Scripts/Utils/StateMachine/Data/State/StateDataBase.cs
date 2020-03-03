using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class StateDataBase : ScriptableObject
{
    public List<TransitionDataBase> Transitions = new List<TransitionDataBase>();

    [ContextMenu("Create Transition")]
    public void ShowTransitionBrowser()
    {
        TypeBrowser currentBrowser = TypeBrowser.ShowBrowser();
        currentBrowser.CreateNewFunc += this.CreateTransition;
        currentBrowser.InitBrowser(typeof(TransitionDataBase));
    }

    public void CreateTransition(System.Type type)
    {
        TransitionDataBase newTrans = ScriptableObject.CreateInstance(type) as TransitionDataBase;
        if (newTrans != null)
        {
            newTrans.OwnerState = this;
            Transitions.Add(newTrans);

            string path = AssetDatabase.GetAssetPath(this);
            AssetDatabase.AddObjectToAsset(newTrans, path);

            AssetDatabase.ImportAsset(path);
        }
    }

    public virtual string GetClassName()
    {
        throw new System.NotImplementedException();
    }
}