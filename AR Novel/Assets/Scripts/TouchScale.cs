using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
// https://unity3d.com/learn/tutorials/topics/mobile-touch/pinch-zoom

public class TouchScale : MonoBehaviour {

    public bool isARSession = true;
    public bool isEnabled = true;
    public bool clamp = true;
    
    [Range(0.0f, 0.1f)]
    public float min = 0.05f;
    
    [Range(0.1f, 2.0f)]
    public float max = 1.0f;
    
    [Range(0.0f, 2.0f)]
    public float scale = 1.0f;
     
    [Range(0.0f, 2.0f)]
    public float sensitivity = 1.0f;
    
    [HideInInspector]
    public bool isScaling;

    protected void OnTouchDrag(Transform transform)
    {
        if (isARSession)
        {
            // If there are two touches or more on the device...
            if (isEnabled && Input.touchCount >= 2)
            {
                float pinchScale = MultiTouchScale() * sensitivity;
                Vector3 scaleVector = Scale(transform.localScale * pinchScale);
                transform.localScale = scaleVector;
                isScaling = true;
            }
            else
            {
                isScaling = false;
            }
        } else {
            Vector3 scaleVector = Scale(new Vector3(scale, scale, scale));
            transform.localScale = scaleVector;
        }
    }
    
    float MultiTouchScale() {
        var center = default(Vector2);
        var lastcenter = default(Vector2);
         
        var totalCenter = Vector2.zero;
        var totalLastCenter = Vector2.zero;
        var count = 0;

        for (int i = Input.touchCount - 1; i >= 0; i--)
        {
            var screenPosition = Input.GetTouch(i).position;
            var screenLastPosition = Input.GetTouch(i).position - Input.GetTouch(i).deltaPosition;
            totalCenter += screenPosition;
            totalLastCenter += screenLastPosition;
            count += 1;
        }
        if (count > 0)
        {
            center = totalCenter / count;
            lastcenter = totalLastCenter / count;
        }
        
        var distance = default(float);
        var lastDistance = default(float);
        var totalDistances = 0.0f;
        var totalLastDistances = 0.0f;
        var distancesCount = 0;
        for (int i = Input.touchCount - 1; i >= 0; i--)
        {
            Vector2 screenLastPosition = Input.GetTouch(i).position - Input.GetTouch(i).deltaPosition;
            totalLastDistances += (lastcenter - screenLastPosition).magnitude;
            totalDistances += (center - Input.GetTouch(i).position).magnitude;
            distancesCount += 1;
        }
        if (count > 0)
        {
            distance = totalDistances / distancesCount;
            lastDistance = totalLastDistances / distancesCount;
        }
        distance = totalDistances / count;
        lastDistance = totalLastDistances / count;
    
        return distance / lastDistance;  
    }
   
    float TwoTouchPinchScale() {
        // Store both touches.
        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);

        // Find the position in the previous frame of each touch.
        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        // Find the magnitude of the vector (the distance) between the touches in each frame.
        float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

        // Find the difference in the distances between each frame.
        //float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
        float deltaMagnitudeScale = touchDeltaMag / prevTouchDeltaMag;
        return deltaMagnitudeScale;
    }

    protected Vector3 Scale(Vector3 scaleValue)
    {
        Vector3 clampedScale = scaleValue;
        clampedScale.x = clamp ? Mathf.Clamp(scaleValue.x, min, max) : scaleValue.x;
        clampedScale.y = clamp ? Mathf.Clamp(scaleValue.y, min, max) : scaleValue.y;
        clampedScale.z = clamp ? Mathf.Clamp(scaleValue.z, min, max) : scaleValue.z;

        scale = (clampedScale.x + clampedScale.y + clampedScale.z) / 3.0f;
        return clampedScale;
    }
}