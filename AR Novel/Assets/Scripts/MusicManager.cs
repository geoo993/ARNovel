using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour {

    public AudioClip[] levelMusicChangeArray;
    private AudioSource audioSource;
    
    static MusicManager instance;
    
    void Awake()
    {
        if (instance != null){
            Destroy(gameObject);
            Debug.Log("Duplicate MusicPlayer self-destructed");
        }else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start () {
        audioSource = GetComponent<AudioSource>();
        //audioSource.volume = PlayerPrefsManager.GetMasterVolume();
    }
    
    void OnEnable() {
      SceneManager.sceneLoaded += OnSceneLoaded;
    }
 
    void OnDisable() {
      SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
         if (scene.buildIndex < levelMusicChangeArray.Length)
        {
            AudioClip thisAudioClip = levelMusicChangeArray[scene.buildIndex];
            Debug.Log("Playing clip: " + thisAudioClip);

            if (thisAudioClip)
            {
                audioSource.clip = thisAudioClip;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
    }
  
    
    public void SetVolume(float volume){
        audioSource.volume = volume;
        PlayerPrefsManager.SetMasterVolume(volume);
    }
    
    public void MuteVolume(bool mute){
        if (mute)
        {
            PlayerPrefsManager.MuteMasterVolume();
        }
        else
        {
            PlayerPrefsManager.ResetMasterVolume();
        }
        audioSource.volume = PlayerPrefsManager.GetMasterVolume();
    }
}
