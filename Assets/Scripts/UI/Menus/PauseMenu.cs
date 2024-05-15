using UnityEngine;
using UnityEngine.Rendering;

public class PauseMenu : MonoBehaviour
{
    public void OnClick_SettingsMenu()
    {
        MenuManager.OpenMenu(Menu.SettingsMenu, gameObject);
        GameController.Instance.SwitchVFXVolume(true);
    }

    public void OnClick_BackToMainMenu()
    {
        MenuManager.OpenMenu(Menu.ExitMenu, gameObject);
    }
    
    public void OnClick_BackToGame()
    {
        gameObject.SetActive(false);
        UIController.Instance.UnPauseGame();
    }
}
