using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [Header("Audio")]
    [SerializeField] private AudioMixer mainMixer;

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sFXVolumeSlider;

    private void Awake()
    {
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sFXVolumeSlider.onValueChanged.AddListener(SetSfxVolume);
    }

    public void SetMasterVolume(float sliderValue)
    {
        mainMixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
    }
    
    public void SetMusicVolume(float sliderValue)
    {
        mainMixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
    }
    
    public void SetSfxVolume(float sliderValue)
    {
        mainMixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
    }

    public void GoBack()
    {
        gameObject.SetActive(false);
    }
}
