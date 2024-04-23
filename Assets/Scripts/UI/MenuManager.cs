using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // ---- / Public Variables / ---- //
    public static GameObject MainMenu, SettingsMenu, ExitMenu, PauseMenu;

    private void Awake()
    {
        MainMenu = transform.Find("MainMenu")?.gameObject;
        SettingsMenu = transform.Find("SettingsMenu")?.gameObject;
        ExitMenu = transform.Find("ExitConfirmationMenu")?.gameObject;
        PauseMenu = transform.Find("PauseMenu")?.gameObject;
    }
    
    public static void OpenMenu(Menu menu, GameObject callingMenu)
    {
        switch (menu)
        {
            case Menu.MainMenu:
                MainMenu.SetActive(true);
                break;
            case Menu.ExitMenu:
                ExitMenu.SetActive(true);
                break;
            case Menu.SettingsMenu:
                SettingsMenu.SetActive(true);
                break;
            case Menu.PauseMenu:
                PauseMenu.SetActive(true);
                break;
        }
        
        callingMenu.SetActive(false);
    }
}
