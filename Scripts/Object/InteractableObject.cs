using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public List<InteractableScene> _AvaliableScenes = new List<InteractableScene>();

    // Start is called before the first frame update
    void Start()
    {
        InitializeScenes();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeScenes()
    {
        foreach(InteractableScene scene in _AvaliableScenes)
        {
            scene.Initialize();
        }
    }
}

[System.Serializable]
public class InteractableScene
{
    [System.Serializable]
    public class ActorSlot
    {
        public GameObject _Actor;
        public Vector3 _SlotRelativePos;
        public TaskBase _RelevantTask;

        public GameObject _TempObject;
        public Color _DebugColor;

        public void FillSlotPos()
        {
            if (_TempObject)
            {
                _SlotRelativePos = _TempObject.transform.localPosition;
                MeshRenderer _meshRenderer = _TempObject.GetComponent<MeshRenderer>();
                if(_meshRenderer)
                {
                    _meshRenderer.material.color = _DebugColor;
                }
            }
        }
    }

    public string _SceneName;
    public int _ActorNumNeeded; //-1 表示任意数字都行
    public List<ActorSlot> _Slots = new List<ActorSlot>();

    public void Initialize()
    {
        foreach(ActorSlot slot in _Slots)
        {
            slot.FillSlotPos();
        }
    }

    public void AssignSlots(List<GameObject> requesters)
    {

    }
}


