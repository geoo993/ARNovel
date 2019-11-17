using UnityEngine;
using System;

public class TouchScaleObject : TouchScale {

    private void Start()
    {
        if (isARSession)
        {
            Vector3 scaleVector = Scale(new Vector3(scale, scale, scale));
            transform.localScale = scaleVector;
        }
    }

    private void Update()
    {
        OnTouchDrag(transform);
    }
    
    private void OnMouseDrag()
    {
        //OnTouchDrag(transform);
    }
}