using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Net;
using Vuforia;

public class UIManager : MonoBehaviour
{
    // Info panel
    private GameObject infoButton;
    private GameObject infoPanel;
    private GameObject infoText;
    private bool isInfoSelected;
    
    // Scale
    private GameObject scaleButton;
    private bool isScaleSelected;

    // Spin
    private GameObject spinButton;
    private bool isSpinSelected;

    // Rotate
    private GameObject rotateButton;
    private bool isRotateSelected;
    
    // Sound Button
    private GameObject musicManager;
    private GameObject soundButton;
    private bool isSoundSelected;


    void Awake()
    {
        musicManager = GameObject.FindGameObjectWithTag("MusicManager");
        scaleButton = GameObject.FindGameObjectWithTag("ScaleButton");
        rotateButton = GameObject.FindGameObjectWithTag("RotateButton");
        spinButton = GameObject.FindGameObjectWithTag("SpinButton");
        soundButton = GameObject.FindGameObjectWithTag("SoundButton");
        infoButton = GameObject.FindGameObjectWithTag("InfoButton");
        infoPanel = GameObject.FindGameObjectWithTag("InfoPanel");
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rotate()
    {
        isRotateSelected = !isRotateSelected;
        if (rotateButton != null) ChangeColor(rotateButton, isRotateSelected, true);
        EnableRotation(isRotateSelected);
    }

    private void EnableRotation(bool enabled) {

    }

    public void Spin()
    {
        isSpinSelected = !isSpinSelected;
        if (spinButton != null) ChangeColor(spinButton, isSpinSelected, true);
        EnableSpinning(isSpinSelected);
    }

    private void EnableSpinning(bool enabled) {

    }

    public void Scale()
    {
        isScaleSelected = !isScaleSelected;
        if (scaleButton != null) ChangeColor(scaleButton, isScaleSelected, true);
        EnableScaling(isScaleSelected);
    }

    private void EnableScaling(bool enabled) {
        
    }

    public void Info()
    {
        isInfoSelected = !isInfoSelected;
        if (infoButton != null) ChangeColor(infoButton, isInfoSelected, true);
        ShowInfo(isInfoSelected);
    }

    private void ShowInfo(bool show) {
        
    }

    public void PlaySound()
    {
        MuteSound();
    }

    public void MuteSound() {
        isSoundSelected = !isSoundSelected;
        if (soundButton != null) ChangeColor(soundButton, isSoundSelected, true);
        if (musicManager != null) musicManager.GetComponent<MusicManager>().MuteVolume(isSoundSelected);
    }

    protected void ChangeColor(GameObject button, bool selected, bool playClip = false) {
        Color color = selected
            ? button.GetComponent<TouchButton>().selectedColor
            : button.GetComponent<TouchButton>().color;
        if (button.GetComponent<TouchButton>().image != null) button.GetComponent<TouchButton>().image.color = color;
        if (button.GetComponent<TouchButton>().text != null) button.GetComponent<TouchButton>().text.color = color;
        
        if (playClip)
        {
            PlaySoundClip(selected ? "click3" : "click1");
        }
    }

    protected void PlaySoundClip(string clip) {
        SoundEffect soundEffect = GetComponent<SoundEffect>();
        if (soundEffect != null)
        {
            soundEffect.PlayAudioClip(clip, false);
        } else {
            Console.Write("We do not have a SoundEffect object");
        }
    }
 
    public void LoadBrowser(string web){
        //if (IsValidURL(web) == false)
        //{
        //    Debug.Log("URL (" + web +") is not valid");
        //    return;
        //}
        
        Application.OpenURL(web);
    }
    
    // https://stackoverflow.com/questions/924679/c-sharp-how-can-i-check-if-a-url-exists-is-valid
    private bool IsValidURL(string url)
    {
         try
         {
             var request = WebRequest.Create(url);
             request.Timeout = 5000;
             request.Method = "HEAD";

             using (var response = (HttpWebResponse)request.GetResponse())
             {
                response.Close();
                return response.StatusCode == HttpStatusCode.OK;
            }
        }
        catch
        { 
            return false;
        }
   }
    
}
