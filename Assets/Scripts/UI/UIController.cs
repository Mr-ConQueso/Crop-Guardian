using UnityEngine;

public class UIController : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [SerializeField] private GameObject pauseScreen;
    
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

    private void Start()
    {
        pauseScreen.SetActive(false);
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
    private void PauseGame()
    {
        _isGamePaused = true;
        
        pauseScreen.SetActive(true);
        
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    private void UnPauseGame()
    {
        _isGamePaused = false;
        
        pauseScreen.SetActive(false);
        
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}