using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

    public GameObject target;
    public bool forwards = true;
    public bool useAxis = false;
    public enum _Axis
    {
        Up, Down, Left, Right, Forward, Backward
    }
    public _Axis axis = _Axis.Forward;
    
    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        Vector3 axisValue = Vector3.forward;
        switch (axis)
        {
            case _Axis.Up:
                axisValue = Vector3.up;
                break;
            case _Axis.Down:
                axisValue = Vector3.down;
                break;
            case _Axis.Right:
                axisValue = Vector3.right;
                break;
            case _Axis.Left:
                axisValue = Vector3.left;
                break;
            case _Axis.Forward:
                axisValue = Vector3.forward;
                break;
            case _Axis.Backward:
                axisValue = Vector3.back;
                break;
        }

        if (useAxis)
        {
            transform.LookAt(forwards ? target.transform.position : -target.transform.position, axisValue);
        }
        else
        {
            transform.LookAt(forwards ? target.transform.position : -target.transform.position);
        }
        
        
        //if (forwards)
        //{
        //    transform.LookAt(target.transform.position - transform.position, Vector3.up);
        //}
        //else
        //{
        //    transform.LookAt(transform.position - target.transform.position, Vector3.up);
        //}
        //transform.rotation = Quaternion.LookRotation(transform.position - target.transform.position);
        
    }
}
