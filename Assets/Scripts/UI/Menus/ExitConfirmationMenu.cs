using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }

    public void OnClick_StayInPauseMenu()
    {
        MenuManager.OpenMenu(Menu.PauseMenu, gameObject);
    }
}
