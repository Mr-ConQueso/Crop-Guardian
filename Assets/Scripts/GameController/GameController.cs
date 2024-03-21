using UnityEngine;

public class GameController : MonoBehaviour
{
    // ---- / Static Variables / ---- //
    private static bool _isGamePaused;
    private static bool _isGameEnded;
    
    // ---- / Serialized Variables / ---- //
    [SerializeField] private GameObject endGameScreen;
    
    private void OnDestroy()
    {
        PlayerController.OnPlayerDeath -= OnPlayerDeathHandler;
    }

    public static bool IsGamePaused()
    {
        return _isGamePaused;
    }
    
    public static bool IsGameEnded()
    {
        return _isGameEnded;
    }
    
    /// <summary>
    /// Pause the time, remove player control
    /// and show the cursor.
    /// </summary>
    public static void PauseGame()
    {
        _isGamePaused = true;
        
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public static void UnPauseGame()
    {
        _isGamePaused = false;
        
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
        
        endGameScreen.SetActive(false);
        
        PlayerController.OnPlayerDeath += OnPlayerDeathHandler;
    }
    
    /// <summary>
    /// Pause the time, remove player control
    /// and show the cursor. End the game.
    /// </summary>
    private void EndGame()
    {
        _isGameEnded = true;
        
        endGameScreen.SetActive(true);
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    private void OnPlayerDeathHandler()
    {
        EndGame();
    }
}
