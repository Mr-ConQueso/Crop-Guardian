using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // ---- / Public Variables / ---- //
    public static GameObject MainMenu, SettingsMenu, ExitMenu, PauseMenu, StoreMenu, CreditsMenu, ControlsMenu;

    private void Awake()
    {
        MainMenu = transform.Find("MainMenu")?.gameObject;
        SettingsMenu = transform.Find("SettingsMenu")?.gameObject;
        ExitMenu = transform.Find("ExitConfirmationMenu")?.gameObject;
        PauseMenu = transform.Find("PauseMenu")?.gameObject;
        StoreMenu = transform.Find("StoreMenu")?.gameObject;
        CreditsMenu = transform.Find("CreditsMenu")?.gameObject;
        ControlsMenu = transform.Find("ControlsMenu")?.gameObject;
    }
    
    /// <summary>
    /// Open the selected Menu and close the current calling this funtion
    /// </summary>
    /// <param name="menu"></param>
    /// <param name="callingMenu"></param>
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
            case Menu.StoreMenu:
                StoreMenu.SetActive(true);
                break;
            case Menu.CreditsMenu:
                CreditsMenu.SetActive(true);
                break;
            case Menu.ControlsMenu:
                ControlsMenu.SetActive(true);
                break;
        }
        
        callingMenu.SetActive(false);
    }
}
