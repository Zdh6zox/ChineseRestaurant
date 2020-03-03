using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventManager : MonoBehaviour
{
    Dictionary<string, InnEvent> m_EventDictionary = new Dictionary<string, InnEvent>();

    public void AddEventSpy(string eventName,UnityAction<BaseInnEventData> action)
    {
        if (!m_EventDictionary.ContainsKey(eventName))
        {
            InnEvent innEvent = new InnEvent();
            innEvent.AddListener(action);
            m_EventDictionary.Add(eventName, innEvent);
        }

        m_EventDictionary[eventName].AddListener(action);
    }

    public void RemoveEventSpy(string eventName, UnityAction<BaseInnEventData> action)
    {
        if (m_EventDictionary.ContainsKey(eventName))
        {
            m_EventDictionary[eventName].RemoveListener(action);
        }
    }

    public void AddUIEventSpy(EventTriggerType eventType,GameObject spyer,UnityAction<BaseEventData> action)
    {
        EventTrigger evtTrigger = spyer.GetComponent<EventTrigger>();
        if(evtTrigger == null)
        {
            evtTrigger = spyer.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry newEntry = new EventTrigger.Entry();
        newEntry.eventID = eventType;
        newEntry.callback.AddListener(action);
        evtTrigger.triggers.Add(newEntry);
    }

    public void RemoveUIEventSpy(EventTriggerType eventType, GameObject spyer)
    {
        EventTrigger evtTrigger = spyer.GetComponent<EventTrigger>();
        if (evtTrigger)
        {
            List<EventTrigger.Entry> entryList = evtTrigger.triggers;
            entryList.Remove(entryList.Find(x => x.eventID == eventType));
        }
    }

    public void BroadcastEvent(BaseInnEventData evt)
    {
        m_EventDictionary[evt.EventName].Invoke(evt);
    }
}
