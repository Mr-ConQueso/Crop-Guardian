using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [SerializeField] private AudioMixer mainMixer;
    
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
    }

}
