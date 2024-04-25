using UnityEngine;
using UnityEngine.Rendering;

public class PauseMenu : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [SerializeField] private Volume cRTVolume;
        
    public void OnClick_SettingsMenu()
    {
        MenuManager.OpenMenu(Menu.SettingsMenu, gameObject);
        cRTVolume.enabled = true;
    }

    public void OnClick_BackToMainMenu()
    {
        MenuManager.OpenMenu(Menu.ExitMenu, gameObject);
    }
    
    public void OnClick_BackToGame()
    {
        gameObject.SetActive(false);
        UIController.UnPauseGame();
    }
}
