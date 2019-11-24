using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIText : MonoBehaviour {

    public GameObject background;
    public GameObject textMesh;
    private bool showText = true;
    private bool fadeText = true;
    private float transitionTime = 1.0f;
    public Color textColor = Color.black;
    public Color backgroundColor = Color.white;
  
	// Use this for initialization
	void Start () {
        if (textMesh != null) Fade(textMesh, Color.clear, false);
        if (background != null) FadeOut(background, false);
    }
    
    // Update is called once per frame
    void Update () {
    
        if (background != null && textMesh != null) {
            textMesh.transform.rotation = Quaternion.LookRotation(textMesh.transform.position - Camera.main.transform.position);
        }

        if (showText == true)
        {
            ChangeColor(textColor, backgroundColor);
            if (fadeText)
            {
                if (textMesh != null) Fade(textMesh, textColor);
                if (background != null) FadeIn(background);
            }
            else
            {
                if (textMesh != null) Fade(textMesh, Color.clear);
                if (background != null) FadeOut(background);
            }
        }
        
    }
    
    public void AddText(string name, Color tColor, Color bColor, float time)
    {
        if (textMesh != null && background != null) {
            textMesh.GetComponent<TextMesh>().text = name;
            textMesh.GetComponent<TextMesh>().color = tColor;
            background.GetComponent<MeshRenderer>().material.color = bColor;
            transitionTime = time;
            fadeText = true;
        }
    }
    
    public void ChangeColor(Color tColor, Color bColor) {
        if (textMesh != null && background != null)
        {
            textMesh.GetComponent<TextMesh>().color = tColor;
            background.GetComponent<MeshRenderer>().material.color = bColor;
        }
    }
    
    public string Text() {
        return (textMesh != null) ? textMesh.GetComponent<TextMesh>().text : "";
    }
    
    public void RemoveText() {
        if (textMesh != null)
        {
            textMesh.GetComponent<TextMesh>().text = "";
        }
        fadeText = false;
    }
    
    public void HideText(bool hide) {
        showText = !hide;
        
        if (background != null) {
            background.GetComponent<MeshRenderer>().enabled = !hide;
        }
        
        if (textMesh != null) {
            textMesh.GetComponent<MeshRenderer>().enabled = !hide;
        }
    }
 
    void Fade (GameObject item, Color toColor, bool animate = true)
    {
        item.GetComponent<MeshRenderer>().material.color = animate ? Color.Lerp(item.GetComponent<MeshRenderer>().material.color, toColor, Time.deltaTime * transitionTime) : toColor;
    }
     
    void FadeIn (GameObject item, bool animate = true)
    {
        float currentAlpha = item.GetComponent<MeshRenderer>().material.GetFloat("_Alpha");
        float time = animate ? Mathf.Lerp(currentAlpha, 0.8f, Time.deltaTime * transitionTime) : 0.8f;
        item.GetComponent<MeshRenderer>().material.SetFloat("_Alpha", time);
    }
    void FadeOut(GameObject item, bool animate = true) {
        float currentAlpha = item.GetComponent<MeshRenderer>().material.GetFloat("_Alpha");
        float time = animate ? Mathf.Lerp(currentAlpha, 0.0f, Time.deltaTime * transitionTime) : 0.0f;
        item.GetComponent<MeshRenderer>().material.SetFloat("_Alpha", time);
    }
    
}
