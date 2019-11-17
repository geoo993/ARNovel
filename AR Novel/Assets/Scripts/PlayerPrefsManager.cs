using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerPrefsManager : MonoBehaviour {

    const string MASTER_VOLUME_KEY = "master_volume";
    const string SOUND_EFFECTS_VOLUME_KEY = "sound_effect_volume";
    const string MARKERLESS_KEY = "markerless";
    const string LEVEL_KEY = "level_unlocked_";

    private static float volumeValue = 1.0f;
    private static float soundEffectVolumeValue = 1.0f;
   
    // Set Master Volume
    public static void SetMasterVolume(float volume){
        if (volume >= 0.0f && volume <= 1.0f)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
            PlayerPrefs.Save();
            volumeValue = volume;
        }else{
            Debug.LogError("Master volume out of range");
        }
    }
    
    public static void MuteMasterVolume(){
        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, 0.0f);
        PlayerPrefs.Save();
    }
    
    public static void ResetMasterVolume(){
        SetMasterVolume(volumeValue);
    }
    
    public static float GetMasterVolume(){
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }
    
    // Set sound effect voulume
    public static void SetSoundEffectVolume(float volume){
        if (volume >= 0.0f && volume <= 1.0f)
        {
            PlayerPrefs.SetFloat(SOUND_EFFECTS_VOLUME_KEY, volume);
            PlayerPrefs.Save();
            soundEffectVolumeValue = volume;
        }else{
            Debug.LogError("Sound Effect volume out of range");
        }
    }
    
    public static void MuteSoundEffectVolume(){
        PlayerPrefs.SetFloat(SOUND_EFFECTS_VOLUME_KEY, 0.0f);
        PlayerPrefs.Save();
    }
    
    public static void ResetSoundEffectVolume(){
        SetSoundEffectVolume(soundEffectVolumeValue);
    }
    
    public static float GetSoundEffectVolume(){
        return PlayerPrefs.GetFloat(SOUND_EFFECTS_VOLUME_KEY);
    }
    
    // Set markerless AR experience
    public static void SetARExperience(bool markerless){
        int markerlessAR = markerless ? 1 : 0;  // use 1 for true and 0 for false
        if (markerlessAR < 2 && markerlessAR >= 0)
        {
            PlayerPrefs.SetInt (MARKERLESS_KEY, markerlessAR);
            PlayerPrefs.Save();
        } else {
            Debug.LogError("Markerless value out of range");
        }
    }
    
    public static bool GetARExperience(){
        return PlayerPrefs.GetInt(MARKERLESS_KEY) == 1;
    }
    
    // UnLocked Level
    public static void UnLockedLevel(int level){
        if (level > 0 && level < SceneManager.sceneCount)
        {
            PlayerPrefs.SetInt(LEVEL_KEY + level.ToString(), 1); // use 1 for true
            PlayerPrefs.Save();
        }else{
            Debug.LogError("Trying to unlocked level that's not in build settings");
        }
    }
    
    public static bool IsLevelUnLocked(int level){
        if (level > 0 && level < SceneManager.sceneCount)
        {
            return (PlayerPrefs.GetInt(LEVEL_KEY + level.ToString()) == 1);
        }else {
            Debug.LogError("Trying to query level that's not in build settings");
            return false;
        }
    }
    
    public static bool SettingsAvailalable() {
        return (PlayerPrefs.HasKey(MASTER_VOLUME_KEY) || PlayerPrefs.HasKey(SOUND_EFFECTS_VOLUME_KEY) || PlayerPrefs.HasKey(MARKERLESS_KEY));
    }
}
