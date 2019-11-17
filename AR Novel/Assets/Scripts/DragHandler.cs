using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public static GameObject objectDragged;
    private Vector3 objectDraggedPosition;
    private Transform objectDraggedParent;
    
    [HideInInspector]
    public Vector3 lastMovedPosition;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        objectDragged = gameObject;
        objectDraggedPosition = this.transform.position;
        objectDraggedParent = this.transform.parent;
        lastMovedPosition = this.transform.position;

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
 
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        transform.position = Input.mousePosition;
        lastMovedPosition = this.transform.position;
    }
 
    public void OnEndDrag(PointerEventData eventData)
    {
        objectDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (transform.parent == objectDraggedParent)
        {
            // return to position
            transform.position = objectDraggedPosition;
        }
        
        //Debug.Log(""+ transform.name +"  "+ transform.position+"    "+lastMovedPosition);
        
    }
    
    
    
    //Method to Return Selected Object
    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 100, out hit))
        {
            target = hit.collider.gameObject;
            Debug.Log(target.name);
        }
        return target;
    }
	
}
