using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseDragable : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public abstract void OnBeginDrag(PointerEventData eventData);

    public abstract void OnDrag(PointerEventData eventData);

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        PassEvent(eventData, ExecuteEvents.dropHandler);
    }

    public void PassEvent<T>(PointerEventData data, ExecuteEvents.EventFunction<T> function)
    where T : IEventSystemHandler
    {
        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(data, results);

        GameObject current = data.pointerDrag;
        for (int i = 0; i < results.Count; i++)
        {
            if (current != results[i].gameObject)
            {
                data.pointerPressRaycast = results[i];
                ExecuteEvents.Execute(results[i].gameObject, data, function);
            }
        }
    }
}
