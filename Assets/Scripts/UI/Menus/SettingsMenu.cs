using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsController : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private TMP_Text audioModeButtonText;
    [SerializeField] private TMP_Text qualityButtonText;
    [SerializeField] private TMP_Text fullScreenButtonText;
    
    [Header("Music")]
    [SerializeField] private MusicController musicController;
    
    // ---- / Private Variables / ---- //
    private int _currentAudioModeIndex;
    private int _currentQualityIndex;
    private int _currentFullScreenModeIndex;
    private AudioConfiguration _audioConfiguration;
    private Resolution[] _resolutions;
    
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

    public void OnClick_GoBack()
    {   
        MenuManager.OpenMenu(MenuManager.MainMenu != null ? Menu.MainMenu : Menu.PauseMenu, gameObject);
        if (GameController.Instance != null)
        {
            GameController.Instance.SwitchVFXVolume(false);
        }
    }

    public void OnClick_ChangeAudioMode()
    {
        _currentAudioModeIndex = CycleNumber(_currentAudioModeIndex, 3);

        switch (_currentAudioModeIndex)
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
        _currentQualityIndex = CycleNumber(_currentQualityIndex, 2);
        QualitySettings.SetQualityLevel(_currentQualityIndex);

        qualityButtonText.text = _currentQualityIndex switch
        {
            0 => "High",
            1 => "Low",
            2 => "Medium",
            _ => "High"
        };
    }
    
    public void OnClick_SetFullScreen()
    {
        _currentFullScreenModeIndex = CycleNumber(_currentFullScreenModeIndex, 2);

        switch (_currentFullScreenModeIndex)
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

    private void SetAudioMode(AudioSpeakerMode speakerMode, String speakerModeText)
    {
        musicController.SaveLastPlayedTime();
        
        _audioConfiguration.speakerMode = speakerMode;
        AudioSettings.Reset(_audioConfiguration);
        audioModeButtonText.text = speakerModeText;
        
        musicController.PLayAtLastPlayedTime();
    }
    
    private void Start()
    {
        _audioConfiguration = AudioSettings.GetConfiguration();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
}
