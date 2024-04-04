using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [Header("In-Game Info")]
    [SerializeField] private TMP_Text survivedTimeText;
    [SerializeField] private TMP_Text scoreText;
    
    [Header("End-Game Screen")]
    [SerializeField] private GameObject endGameScreen;
    [SerializeField] private TMP_Text survivedTimeScoreText;
    [SerializeField] private TMP_Text maxScoreText;
    
    // ---- / Static Variables / ---- //
    private static bool _isGameEnded;
    private float _elapsedTime;
    private bool _isTimerRunning;
    private const string HighestSurviveTimeKey = "HighestSurviveTimeKey";
    private const string HighScoreKey = "HighScore";
    private static float _highestSurviveTime;
    private static int _currentScore;
    private int _highScore;
    
    private void OnDestroy()
    {
        PlayerController.OnPlayerDeath -= OnPlayerDeathHandler;
    }
    
    public static bool IsGameEnded()
    {
        return _isGameEnded;
    }

    public static void AddScore(int amount)
    {
        _currentScore += amount;
    }

    private void StartTimer()
    {
        _isTimerRunning = true;
    }

    private void StopTimer()
    {
        _isTimerRunning = false;
    }
    
    private void UpdateSurvivedTime(float newScore)
    {
        if (newScore > _highestSurviveTime)
        {
            _highestSurviveTime = newScore;
            
            PlayerPrefs.SetFloat(HighestSurviveTimeKey, _highestSurviveTime);
            PlayerPrefs.Save();
        }
    }
    
    private void UpdateHighScore(int newScore)
    {
        if (newScore > _highScore)
        {
            _highScore = newScore;
            
            PlayerPrefs.SetFloat(HighestSurviveTimeKey, _highScore);
            PlayerPrefs.Save();
        }
    }
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
        
        endGameScreen.SetActive(false);
        StartTimer();
        
        _highestSurviveTime = PlayerPrefs.GetFloat(HighestSurviveTimeKey, 0f);
        _highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        
        PlayerController.OnPlayerDeath += OnPlayerDeathHandler;
    }
    
    private void Update()
    {
        scoreText.text = _currentScore.ToString();
        
        if (_isTimerRunning)
        {
            _elapsedTime += Time.deltaTime;
            survivedTimeText.text = FormatTimer(_elapsedTime);
        }
    }
    
    /// <summary>
    /// Pause the time, remove player control
    /// and show the cursor. End the game.
    /// </summary>
    private void EndGame()
    {
        _isGameEnded = true;

        StopTimer();
            
        endGameScreen.SetActive(true);
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        UpdateSurvivedTime(_elapsedTime);
        UpdateHighScore(_currentScore);
        
        survivedTimeScoreText.text = FormatTimer(_highestSurviveTime);
        maxScoreText.text = _highScore.ToString();
    }
    
    private void OnPlayerDeathHandler()
    {
        EndGame();
    }

    private string FormatTimer(float time)
    {
        // Format the elapsed time as minutes:seconds:milliseconds
        string minutes = Mathf.Floor(time / 60).ToString("00");
        string seconds = Mathf.Floor(time % 60).ToString("00");
        //string milliseconds = Mathf.Floor((elapsedTime * 1000) % 1000).ToString("000");

        return minutes + ":" + seconds;
    }
}
