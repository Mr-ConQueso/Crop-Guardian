using UnityEngine;

public class UIController : MonoBehaviour
{
    // ---- / Static Variables / ---- //
    private static bool _isGamePaused;
    
    public static bool IsGamePaused()
    {
        return _isGamePaused;
    }

    public void SetGamePaused()
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetGamePaused();
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