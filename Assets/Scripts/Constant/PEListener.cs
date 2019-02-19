using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
/// <summary>
/// UI时间监听
/// </summary>
public class PEListener : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler {

    public Action<PointerEventData> OnClickDown;
    public Action<PointerEventData> OnClickUp;
    public Action<PointerEventData> OnClickDrag;
    /// <summary>
    /// 手指/鼠标按下 事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnClickDown != null)
        {
            OnClickDown(eventData);
        }
    }
    /// <summary>
    /// 手指抬起/鼠标抬起 事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnClickUp != null)
        {
            OnClickUp(eventData);
        }
    }
    /// <summary>
    /// 手指拖拽/鼠标拖拽 事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (OnClickDrag != null)
        {
            OnClickDrag(eventData);
        }
    }

}
