
using UnityEngine;
using System;

public class TouchRotateObject : TouchRotate {
   
    private void OnMouseDrag()
    {
        OnTouchDrag(transform);
    }

    private void OnMouseUp()
    {
        OnTouchEnd();
    }
}