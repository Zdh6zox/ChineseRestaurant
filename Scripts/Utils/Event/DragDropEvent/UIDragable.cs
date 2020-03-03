using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragable : BaseDragable
{
    //存储图片中心点与鼠标点击点的偏移量
    private Vector3 m_offset;

    //存储当前拖拽图片的RectTransform组件
    private RectTransform m_rt;

    void Start()
    {
        //初始化
        m_rt = gameObject.GetComponent<RectTransform>();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        // 存储点击时的鼠标坐标
        Vector3 tWorldPos;
        //UI屏幕坐标转换为世界坐标
        RectTransformUtility.ScreenPointToWorldPointInRectangle(m_rt, eventData.position, eventData.pressEventCamera, out tWorldPos);
        //计算偏移量   
        m_offset = transform.position - tWorldPos;

        SetDraggedPosition(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }

    private void SetDraggedPosition(PointerEventData eventData)
    {
        //存储当前鼠标所在位置
        Vector3 globalMousePos;
        //UI屏幕坐标转换为世界坐标
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_rt, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            //设置位置及偏移量
            m_rt.position = globalMousePos + m_offset;
        }
    }
}
