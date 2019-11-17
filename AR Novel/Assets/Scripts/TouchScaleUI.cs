using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class TouchScaleUI : TouchScale, IBeginDragHandler, IDragHandler, IEndDragHandler {
    
    public Transform target;
    
    void Start()
    {
        if (isARSession && target != null)
        {
            Vector3 scaleVector = Scale(new Vector3(scale, scale, scale));
            target.transform.localScale = scaleVector;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Console.Write("Drag Begin "+ Input.touchCount+"\n");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(target != null) OnTouchDrag(target);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Console.Write("Drag Ended "+ Input.touchCount+"\n");
    }

}