using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
// https://www.youtube.com/watch?v=Bqcu94VuVOI

[RequireComponent(typeof(LineRenderer))]
public class UILine : MonoBehaviour {

    private LineRenderer lineRenderer;
    
    public bool isEnabled = true;
    public Transform target;
    public Transform destination;
    public float lineWidth;
    public float lineDrawSpeed;
    public Color lineColor = Color.white;
    
    // Use this for initialization
    void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        ChangeColor(lineColor);
    }

    // Update is called once per frame
    void Update()
    {
        if (destination != null && isEnabled)
        {
            lineRenderer.SetPosition(0, target.transform.position);
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;

            Vector3 pointA = target.transform.position;
            Vector3 pointB = destination.transform.position;
            float distance = Vector3.Distance(pointB, pointA);
            Vector3 destinationDirection = Vector3.Normalize(pointB - pointA);
            //transform.rotation = Quaternion.LookRotation(destinationDirection, Vector3.up);

            // Get the unit vector in the desired direction, multiply by the desired length and add the starting point.
            Vector3 pointAlongLine = pointA + destinationDirection * distance;
            lineRenderer.SetPosition(1, pointAlongLine);
            
            /*
            // Add text
            float originRadius = origin.GetComponent<MeshRenderer>().bounds.extents.magnitude;
            float destinationRadius = destination.GetComponent<MeshRenderer>().bounds.extents.magnitude;
            float originToDistance = (distance - originRadius - destinationRadius) / 2.0f;
            Vector3 textPositionAlongLine = pointA + destinationDirection * (originRadius + originToDistance);
            Vector3 destinationDirectionRight = Vector3.Cross(destinationDirection, transform.up);
            textMesh.transform.position = new Vector3(textPositionAlongLine.x, textPositionAlongLine.y + lineWidth, textPositionAlongLine.z);
            textMesh.transform.rotation = Quaternion.LookRotation(destinationDirectionRight, transform.up);

            float distanceToSun = SolarSystemModel.distanceToSun[destination.name];
            textMesh.GetComponent<TextMesh>().text = "" + distanceToSun + " million km";
            */
        } else {
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);
            lineRenderer.startWidth = 0.0f;
            lineRenderer.endWidth = 0.0f;
        }
    }
    
    public void ChangeColor(Color color) {
        if (lineRenderer != null)
        {
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
        }
    }
    
    public void AddLine(Transform fromTarget, Transform toDestination, float width, float speed, Color color) {
        target = fromTarget;
        destination = toDestination;
        lineWidth = width;
        lineDrawSpeed = speed;
        ChangeColor(color);
    }
    
    public void RemoveLine() {
        target = null;
        destination = null;
        lineWidth = 0.0f;
        lineDrawSpeed = 0.0f;
        ChangeColor(lineColor);
    }
     
}
