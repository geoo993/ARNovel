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

// https://www.youtube.com/watch?v=R6Iaa6lvGwY
public class UIManager : MonoBehaviour
{
    
    // Right Panel
    private GameObject rightPanel;
    
    // Left panel
    private GameObject leftPanel;

    // Info panel
    private GameObject infoButton;
    private GameObject infoPanel;
    private GameObject infoText;
    private bool isInfoSelected;

    // Title
    private GameObject titleText;
    private GameObject subtitleText;
    
    // Scale
    private GameObject scaleButton;
    private bool isScaleSelected = true;

    // Rotate
    private GameObject rotateButton;
    private bool isRotateSelected = true;

    // Info Sound Button
    private GameObject introSoundButton;
    private bool isIntroSoundSelected;
    
    // Sound Button
    private GameObject musicManager;
    private GameObject soundButton;
    private bool isSoundSelected;

    // Target Tracker
    public static TargetTracker target;
    private TrackableBehaviour currentBehaviour;

    void Awake()
    {
        Debug.Log ("DEBUG LOG: STARTING DEMO\n");
        Console.Write ("CONSOLE WRITE: STARTING DEMO\n");
        musicManager = GameObject.FindGameObjectWithTag("MusicManager");
        scaleButton = GameObject.FindGameObjectWithTag("ScaleButton");
        rotateButton = GameObject.FindGameObjectWithTag("RotateButton");
        soundButton = GameObject.FindGameObjectWithTag("SoundButton");
        introSoundButton = GameObject.FindGameObjectWithTag("IntroSoundButton");
        infoButton = GameObject.FindGameObjectWithTag("InfoButton");
        infoPanel = GameObject.FindGameObjectWithTag("InfoPanel");
        infoText = GameObject.FindGameObjectWithTag("InfoText");
        rightPanel = GameObject.FindGameObjectWithTag("RightPanel");
        leftPanel = GameObject.FindGameObjectWithTag("LeftPanel");
        titleText = GameObject.FindGameObjectWithTag("TitleText");
        subtitleText  = GameObject.FindGameObjectWithTag("SubTitleText");
    }

    void Start()
    {
        ShowInfo(isInfoSelected);
        EnableRotation(isRotateSelected);
        EnableScaling(isScaleSelected);
    }

    // Update is called once per frame
    void Update() {

        if (target) {
            switch (target.ARNovelMode) {
                case TargetTracker.Level.LevelOne:
                    SetTitle("Part One", "Big Vegas");
                    break;
                case TargetTracker.Level.LevelTwo:
                    SetTitle("Part Two", "Rocket");
                    break;
                case TargetTracker.Level.LevelThree:
                    SetTitle("Part Three", "Spacecraft");
                    break;
                default: 
                    SetTitle("AR Novel", "Dark Nebula");
                break;
            }

        }
    }

    private void SetTitle(string title, string subtitle) {
        if (titleText != null) titleText.GetComponent<Text>().text = title;
        if (subtitleText != null) subtitleText.GetComponent<Text>().text = subtitle;
    }

    public void Rotate()
    {
        isRotateSelected = !isRotateSelected;
        if (rotateButton != null) ChangeColor(rotateButton, isRotateSelected, true);
        EnableRotation(isRotateSelected);
    }

    private void EnableRotation(bool enabled) {
        foreach (TouchRotateObject touchRotate in FindObjectsOfType<TouchRotateObject>())
        {
            touchRotate.isEnabled = enabled;
        }
    }

    public void Scale()
    {
        isScaleSelected = !isScaleSelected;
        if (scaleButton != null) ChangeColor(scaleButton, isScaleSelected, true);
        EnableScaling(isScaleSelected);
    }

    private void EnableScaling(bool enabled) {
        foreach (TouchScaleObject touchScale in FindObjectsOfType<TouchScaleObject>())
        {
            touchScale.isEnabled = enabled;
        }
    }

    public void Info()
    {
        isInfoSelected = !isInfoSelected;
        if (infoButton != null) ChangeColor(infoButton, isInfoSelected, true);
        ShowInfo(isInfoSelected);
    }

    private void ShowInfo(bool show) {
        if (infoPanel != null) infoPanel.SetActive(infoButton != null && show);
    }

    private void SetInfo() {
        
        if (infoText) {
            TMPro.TextMeshProUGUI textMesh = infoText.GetComponent<TMPro.TextMeshProUGUI>();
            //textMesh.text = SpaceXRocketModel.RocketParts[SpaceXRocketPart.currentSelected.gameObject.name];
        }
    }

    public void MuteSound() {
        if (!isIntroSoundSelected) {
            isSoundSelected = !isSoundSelected;
            if (soundButton != null) ChangeColor(soundButton, isSoundSelected, true);
            if (musicManager != null) musicManager.GetComponent<MusicManager>().MuteVolume(isSoundSelected);
        }
    }

    public void PlayIntro() {
        isIntroSoundSelected = !isIntroSoundSelected;
        if (introSoundButton != null) ChangeColor(introSoundButton, isIntroSoundSelected, true);
        PlayIntroSound(isIntroSoundSelected);
    }

    private void PlayIntroSound(bool selected) {
        if (selected) {
            if (musicManager != null) musicManager.GetComponent<MusicManager>().MuteVolume(true);
            PlaySoundClip("intro");
        } else {
            if (!isSoundSelected) {
                if (musicManager != null) musicManager.GetComponent<MusicManager>().MuteVolume(false);
                StopSoundClip();
            }
        }
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

    private void PlaySoundClip(string clip) {
        SoundEffect soundEffect = GetComponent<SoundEffect>();
        if (soundEffect != null)
        {
            soundEffect.PlayAudioClip(clip, false);
        } else {
            Console.Write("We do not have a SoundEffect object");
        }
    }

     private void StopSoundClip() {
        SoundEffect soundEffect = GetComponent<SoundEffect>();
        if (soundEffect != null)
        {
            soundEffect.StopAudioClip();
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
