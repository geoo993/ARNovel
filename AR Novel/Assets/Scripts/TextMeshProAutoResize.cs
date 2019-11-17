
using UnityEngine;

public class TextMeshProAutoResize : MonoBehaviour {

    protected Vector2 originalSize;
    
	// Use this for initialization
	void Start () {
        originalSize = GetComponent<RectTransform>().sizeDelta;
	}
	
	// Update is called once per frame
	void Update () {
        TMPro.TextMeshProUGUI textMesh = GetComponent<TMPro.TextMeshProUGUI>();
        if (textMesh != null)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, textMesh.preferredHeight);
        }
	}
}
