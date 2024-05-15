using BaseGame;
using UnityEngine;

public class ExitConfirmationMenu : MonoBehaviour
{
    public void OnClick_ExitGame()
    {
        Application.Quit();
    }

    public void OnClick_StayMainMenu()
    {
        MenuManager.OpenMenu(Menu.MainMenu, gameObject);
    }
    
    public void OnClick_BackToMainMenu()
    {
        SceneSwapManager.SwapScene("StartMenu");
    }

    public void OnClick_StayInPauseMenu()
    {
        MenuManager.OpenMenu(Menu.PauseMenu, gameObject);
    }
}
