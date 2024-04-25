using UnityEngine;

public class UIController : MonoBehaviour
{
    // ---- / Static Variables / ---- //
    private static bool _isGamePaused;
    
    public static bool IsGamePaused()
    {
        return _isGamePaused;
    }

    private static void TogglePauseState()
    {
        if (_isGamePaused)
        {
            UnPauseGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && 
            (MenuManager.SettingsMenu.activeSelf == false && MenuManager.ExitMenu.activeSelf == false))
        {
            TogglePauseState();
        }
    }

    /// <summary>
    /// Pause the time, remove player control
    /// and show the cursor.
    /// </summary>
    public static void PauseGame()
    {
        _isGamePaused = true;
        
        MenuManager.PauseMenu.SetActive(true);
        
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public static void UnPauseGame()
    {
        _isGamePaused = false;
        
        MenuManager.PauseMenu.SetActive(false);
        
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}