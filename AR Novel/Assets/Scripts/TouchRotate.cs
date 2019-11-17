using UnityEngine;
using System;

// https://www.youtube.com/watch?v=S3pjBQObC90

public class TouchRotate : MonoBehaviour {
    
    public bool isEnabled = true;
    
    [Range(0.0f, 100.0f)]
    public float sensitivity = 10.0f;
    
    [HideInInspector]
    public bool isRotating;

    public enum _Axis
    {
        X , Y, Z, All 
    }
    public _Axis axis = _Axis.All;

    protected void OnTouchDrag(Transform transfrom)
    {
        if (isEnabled)
        {
            float rotX = Input.GetAxis("Mouse X") * sensitivity; //* Mathf.Deg2Rad;
            float rotY = Input.GetAxis("Mouse Y") * sensitivity; //* Mathf.Deg2Rad;
            
            switch (axis)
            {
                case _Axis.X:
                    transfrom.Rotate(Vector3.right, rotY, Space.Self);
                    break;
                case _Axis.Y:
                    transfrom.Rotate(Vector3.up, -rotX, Space.Self);
                    break;
                 case _Axis.Z:
                    transfrom.Rotate(Vector3.forward, -rotX, Space.Self);
                    break;
                case _Axis.All:
                    transfrom.Rotate(Vector3.up, -rotX, Space.Self);
                    transfrom.Rotate(Vector3.right, rotY, Space.Self);
                    break;
            }
            isRotating = true;
        }
    }

    protected void OnTouchEnd()
    {
        isRotating = false;
    }
}