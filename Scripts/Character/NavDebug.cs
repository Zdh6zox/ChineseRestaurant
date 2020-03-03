using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavDebug : MonoBehaviour
{
    public NavMeshAgent _Agent;
    public LineRenderer _LineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _Agent = GetComponent<NavMeshAgent>();
        _LineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_Agent)
        {
            if(_Agent.path.corners.Length >1)
            {
                _LineRenderer.SetPositions(_Agent.path.corners);
            }
        }
    }
}
