using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

    public Slider volumeSlider;
    public Slider buttonsVolumeSlider;
    //public Toggle markerlessToggle;

    private MusicManager musicManager;
    
	// Use this for initialization
	void Start () {
        musicManager = FindObjectOfType<MusicManager>();
        volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
        buttonsVolumeSlider.value = PlayerPrefsManager.GetSoundEffectVolume();
        //markerlessToggle.isOn = PlayerPrefsManager.GetARExperience();
        
	}
	
	// Update is called once per frame
	void Update () {
        musicManager.SetVolume(volumeSlider.value);
        //markerlessToggle.onValueChanged.AddListener(OnValueChanged);
    }
    
    void OnValueChanged(bool value) {
        PlayerPrefsManager.SetARExperience(value);
    }
    
    public void SaveAndExit() {
        PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
        PlayerPrefsManager.SetSoundEffectVolume(buttonsVolumeSlider.value);
        //PlayerPrefsManager.SetARExperience(markerlessToggle.isOn);
    }
    
    public void SetDefault() {
        PlayerPrefsManager.SetMasterVolume(0.6f);
        PlayerPrefsManager.SetSoundEffectVolume(0.4f);
        PlayerPrefsManager.SetARExperience(false);
    }
}
