using System;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(AudioSource))]
public class SoundEffect : MonoBehaviour {
    
    AudioSource AudioSource {
        get { return GetComponent<AudioSource>(); }
    }
    
    public AudioClip[] clips;

    void Start()
    {
        // change this to slider in the main menu
        //AudioSource.volume = PlayerPrefsManager.GetSoundEffectVolume();
    }

    // Method that runs when level is loaded
    public void PlayAudioClip(string name, bool loop)
    {
        if (clips != null && clips.Length > 0 && clips.Select(clip => clip.name).ToList().Contains(name) )
        {
            AudioClip thisAudioClip = clips.First(clip => clip.name == name);
            Console.WriteLine("Playing clip: " + thisAudioClip);
            //Debug.Log("Playing clip: " + thisAudioClip);

            if (thisAudioClip)
            {
                AudioSource.clip = thisAudioClip;
                AudioSource.loop = loop;
                AudioSource.Play();
            }
        }
    }

    public void StopAudioClip(){
        AudioSource.Stop();
    }
    
    public void SetVolume(float volume){
        AudioSource.volume = volume;
        PlayerPrefsManager.SetSoundEffectVolume(volume);
    }
    
    public void MuteVolume(bool mute){
        if (mute)
        {
            PlayerPrefsManager.MuteSoundEffectVolume();
        }
        else
        {
            PlayerPrefsManager.ResetSoundEffectVolume();
        }
        AudioSource.volume = PlayerPrefsManager.GetSoundEffectVolume();
    }
}
