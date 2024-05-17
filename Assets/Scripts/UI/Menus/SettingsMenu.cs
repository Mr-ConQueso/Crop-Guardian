using System;
using BaseGame;
using SaveLoad;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour, ISaveable
{
    // ---- / Serialized Variables / ---- //
    [Header("Audio")]
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    
    [Header("Buttons")]
    [SerializeField] private TMP_Text audioModeButtonText;
    [SerializeField] private TMP_Text qualityButtonText;
    [SerializeField] private TMP_Text fullScreenButtonText;
    
    [Header("Music")]
    [SerializeField] private MusicController musicController;
    
    // ---- / Private Variables / ---- //
    private AudioConfiguration _audioConfiguration;
    private Resolution[] _resolutions;
    
    public void SetMasterVolume(float sliderValue)
    {
        mainMixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
        SavedSettings.masterVolume = sliderValue;
    }
    
    public void SetMusicVolume(float sliderValue)
    {
        mainMixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
        SavedSettings.musicVolume = sliderValue;
    }
    
    public void SetSfxVolume(float sliderValue)
    {
        mainMixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
        SavedSettings.sfxVolume = sliderValue;
    }

    public void OnClick_ControlsSettings()
    {
        MenuManager.OpenMenu(Menu.ControlsMenu, gameObject);
    }

    public void OnClick_GoBack()
    {   
        SaveLoadManager.Save();
        
        MenuManager.OpenMenu(MenuManager.MainMenu != null ? Menu.MainMenu : Menu.PauseMenu, gameObject);
        if (GameController.Instance != null)
        {
            GameController.Instance.SwitchVFXVolume(false);
        }
    }

    public void OnClick_ChangeAudioMode(bool addValue)
    {
        if (addValue)
        {
            SavedSettings.audioMode = CycleNumber(SavedSettings.audioMode, 3);
        }

        switch (SavedSettings.audioMode)
        {
            case 0:
                SetAudioMode(AudioSpeakerMode.Stereo, "Stereo");
                break;
            case 1:
                SetAudioMode(AudioSpeakerMode.Mono, "Mono");
                break;
            case 2:
                SetAudioMode(AudioSpeakerMode.Mode5point1, "5.1 Surround");
                break;
            case 3:
                SetAudioMode(AudioSpeakerMode.Mode7point1, "7.1 Surround");
                break;
            default:
                SetAudioMode(AudioSpeakerMode.Stereo, "Stereo");
                break;
        }
    }

    public void OnClick_SetQuality()
    {
        SavedSettings.graphicsQuality = CycleNumber(SavedSettings.graphicsQuality, 2);
        SetQuality();
    }
    
    public void OnClick_SetFullScreen(bool addValue)
    {
        if (addValue)
        {
            SavedSettings.fullScreenMode = CycleNumber(SavedSettings.fullScreenMode, 2);
        }

        switch (SavedSettings.fullScreenMode)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                fullScreenButtonText.text = "Fullscreen";
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                fullScreenButtonText.text = "Windowed";
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                fullScreenButtonText.text = "Maximized";
                break;
            default:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                fullScreenButtonText.text = "Fullscreen";
                break;
        }
    }

    public void OnClick_SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
    }

    private void SetQuality()
    {
        QualitySettings.SetQualityLevel(SavedSettings.graphicsQuality);

        qualityButtonText.text = SavedSettings.graphicsQuality switch
        {
            0 => "High",
            1 => "Low",
            2 => "Medium",
            _ => "High"
        };
    }

    private void SetAudioMode(AudioSpeakerMode speakerMode, String speakerModeText)
    {
        if (musicController != null)
        {
            musicController.SaveLastPlayedTime();
        }
        
        _audioConfiguration.speakerMode = speakerMode;
        AudioSettings.Reset(_audioConfiguration);
        audioModeButtonText.text = speakerModeText;
        
        SetMasterVolume(masterSlider.value);
        SetMusicVolume(musicSlider.value);
        SetSfxVolume(sfxSlider.value);
        
        if (musicController != null)
        {
            musicController.PlayAtLastPlayedTime();
        }
    }
    
    private void Start()
    {
        _audioConfiguration = AudioSettings.GetConfiguration();
    }

    private void OnEnable()
    {
        SaveLoadManager.Load();
    }

    private void Update()
    {
        if (InputManager.WasEscapePressed)
        {
            OnClick_GoBack();
        }
    }
    
    /*
    /// <summary>
    /// Initialize the resolution dropdown and current resolution
    /// </summary>
    private void InitResolution()
    {
        _resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        
        _resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height;
            options.Add(option);

            if (_resolutions[i].width == Screen.currentResolution.width && 
                _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    */

    /// <summary>
    /// Cycle between a given number and a maximum one
    /// </summary>
    /// <param name="numberToCycle"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    private int CycleNumber(int numberToCycle, int maxValue)
    {
        if (numberToCycle < maxValue)
        {
            return numberToCycle + 1;
        }

        return 0;
    }

    public object CaptureState()
    {
        return new SaveData()
        {
            audioMode = audioModeButtonText.text,
            graphicsQuality = qualityButtonText.text,
            fullScreenMode = fullScreenButtonText.text,
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        audioModeButtonText.text = saveData.audioMode;
        qualityButtonText.text = saveData.graphicsQuality;
        fullScreenButtonText.text = saveData.fullScreenMode;

        masterSlider.value = SavedSettings.masterVolume;
        musicSlider.value = SavedSettings.musicVolume;
        sfxSlider.value = SavedSettings.sfxVolume;
    }
    
    [Serializable]
    private struct SaveData
    {
        public string audioMode;
        public string graphicsQuality;
        public string fullScreenMode;
    }
}
