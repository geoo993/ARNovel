using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class TouchRotateUI : TouchRotate, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public Transform target;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Console.Write("Drag Begin\n");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Console.Write("Dragging\n");
        if (target != null) OnTouchDrag(target);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Console.Write("Drag Ended\n");
        OnTouchEnd();
    }

}