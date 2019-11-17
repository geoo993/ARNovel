using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    public bool shouldRotate = true;
    
    public enum _Axis
    {
        X , Y, Z 
    }
    public _Axis axis = _Axis.Y;
    public float speed = 1f;

	void Update () {

        if (shouldRotate)
        {
            switch (axis)
            {
                case _Axis.X:
                    this.transform.Rotate(speed * Time.deltaTime, 0.0f, 0.0f);
                    break;
                case _Axis.Y:
                    this.transform.Rotate(0.0f, speed * Time.deltaTime, 0.0f);
                    break;
                case _Axis.Z:
                    this.transform.Rotate(0.0f, 0.0f, speed * Time.deltaTime);
                    break;
            }
        }
	}
}
