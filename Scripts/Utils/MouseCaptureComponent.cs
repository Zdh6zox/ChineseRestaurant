using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//用于需要获取鼠标事件的GameObject
public class MouseCaptureComponent : MonoBehaviour
{
    private bool m_IsEnableMouseEvent = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ToggleMouseEventCapture(bool isEnable)
    {
        m_IsEnableMouseEvent = isEnable;
    }

    public void TestOnMouseEnter()
    {
        Debug.Log(string.Format("Event Trigger{0} mouse enter event", name));
    }

    private void OnMouseOver()
    {
        if (m_IsEnableMouseEvent == false)
            return;
        //Do the mouse stay operations
    }

    private void OnMouseEnter()
    {
        if (m_IsEnableMouseEvent == false)
            return;
        //Do the mouse enter operations
        Debug.Log(string.Format("{0} mouse enter event", name));
    }

    private void OnMouseExit()
    {
        if (m_IsEnableMouseEvent == false)
            return;
        //Do the mouse exit operations
        Debug.Log(string.Format("{0} mouse exit event", name));
    }

    private void OnMouseDown()
    {
        if (m_IsEnableMouseEvent == false)
            return;
    }

    private void OnMouseUpAsButton()
    {
        if (m_IsEnableMouseEvent == false)
            return;
        //这个只能监测左键事件
        Debug.Log(string.Format("{0} left mouse btn up event", name));
    }
}
