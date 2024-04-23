using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void OnClick_SettingsMenu()
    {
        MenuManager.OpenMenu(Menu.SettingsMenu, gameObject);
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
