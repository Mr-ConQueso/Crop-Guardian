using UnityEngine;

public class UIController : MonoBehaviour
{
    // ---- / Singleton / ---- //
    public static UIController Instance;
    
    // ---- / Public Variables / ---- //
    public bool IsGamePaused;
    
    /// <summary>
    /// Pause the time, remove player control
    /// and show the cursor.
    /// </summary>
    public void PauseGame()
    {
        IsGamePaused = true;
        
        MenuManager.PauseMenu.SetActive(true);
        
        //Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void UnPauseGame()
    {
        IsGamePaused = false;
        
        MenuManager.PauseMenu.SetActive(false);
        
        //Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && 
            (MenuManager.SettingsMenu.activeSelf == false && MenuManager.ExitMenu.activeSelf == false && GameController.IsGameEnded == false))
        {
            TogglePauseState();
        }
    }
    
    private void TogglePauseState()
    {
        if (IsGamePaused)
        {
            UnPauseGame();
        }
        else
        {
            PauseGame();
        }
    }
}