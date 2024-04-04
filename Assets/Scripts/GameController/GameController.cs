using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [SerializeField] private GameObject endGameScreen;
    [SerializeField] private TMP_Text survivedTimeText;
    
    // ---- / Static Variables / ---- //
    private static bool _isGamePaused;
    private static bool _isGameEnded;
    private float _elapsedTime;
    private bool _isRunning;
    private const string HighScoreKey = "HighestSurviveTime";
    private static float _highestSurviveTime;
    
    private void OnDestroy()
    {
        PlayerController.OnPlayerDeath -= OnPlayerDeathHandler;
    }
    
    public void StartTimer()
    {
        _isRunning = true;
    }

    public void StopTimer()
    {
        _isRunning = false;
    }

    public void ResetTimer()
    {
        _elapsedTime = 0f;
        UpdateTimerDisplay();
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
    
    private void UpdateSurvivedTime(float newScore)
    {
        if (newScore > _highestSurviveTime)
        {
            // Update the highest survive time score
            _highestSurviveTime = newScore;
            
            // Save the new high score to PlayerPrefs
            PlayerPrefs.SetFloat(HighScoreKey, _highestSurviveTime);
            PlayerPrefs.Save(); // Save changes to disk immediately
        }
    }
    
    public float GetHighestSurviveTime()
    {
        return _highestSurviveTime;
    }
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
        
        endGameScreen.SetActive(false);
        StartTimer();
        
        PlayerController.OnPlayerDeath += OnPlayerDeathHandler;
        
        _highestSurviveTime = PlayerPrefs.GetFloat(HighScoreKey, 0f);
    }
    
    private void Update()
    {
        if (_isRunning)
        {
            _elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
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

        UpdateSurvivedTime(_elapsedTime);
    }
    
    private void OnPlayerDeathHandler()
    {
        EndGame();
    }

    private void UpdateTimerDisplay()
    {
        // Format the elapsed time as minutes:seconds:milliseconds
        string minutes = Mathf.Floor(_elapsedTime / 60).ToString("00");
        string seconds = Mathf.Floor(_elapsedTime % 60).ToString("00");
        //string milliseconds = Mathf.Floor((elapsedTime * 1000) % 1000).ToString("000");

        // Update the timer text display
        survivedTimeText.text = minutes + ":" + seconds;
    }
}
