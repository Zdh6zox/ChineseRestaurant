using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseInnEventData 
{
    public string EventName;

    public GameObject EventSource;

    public GameObject EventTarget; 
}

public class InnEvent : UnityEvent<BaseInnEventData>
{

}
