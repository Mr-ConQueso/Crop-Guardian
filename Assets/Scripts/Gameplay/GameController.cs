using BaseGame;
using Enemy;
using SaveLoad;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class GameController : MonoBehaviour
{
    // ---- / Singleton / ---- //
    public static GameController Instance;
    
    // ---- / Static Variables / ---- //
    public static bool IsGameEnded;
    
    public int CurrentScore { get; private set; }
    
    // ---- / Serialized Variables / ---- //
    [Header("In-Game Info")]
    [SerializeField] private TMP_Text survivedTimeText;
    [SerializeField] private TMP_Text scoreText;
    
    [Header("Win Level Screen")]
    [SerializeField] private GameObject winLevelScreen;
    
    [Header("End-Game Screen")]
    [SerializeField] private GameObject endGameScreen;
    
    [Header("Volumes")]
    [SerializeField] private Volume globalVolume;
    [SerializeField] private Volume menusVolume;
    
    // ---- / Private Variables / ---- //
    private BaseEnemySpawn _enemySpawner;
    private float _elapsedTime;
    private bool _isTimerRunning;

    public void SwitchVFXVolume(bool isMenusVolumeActive)
    {
        menusVolume.enabled = isMenusVolumeActive;
        globalVolume.enabled = !isMenusVolumeActive;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        PlayerController.OnPlayerDeath -= OnPlayerDeathHandler;
        BossEnemy.OnDefeatBoss -= OnDefeatBossHandler;
        EnemyController.OnDefeatEnemy -= OnDefeatEnemyHandler;
    }

    private void StartTimer()
    {
        _isTimerRunning = true;
    }

    private void StopTimer()
    {
        _isTimerRunning = false;
    }
    
    private float GetHighestSurvivedTime(float oldTime, float newTime)
    {
        if (newTime > oldTime)
        {
            return newTime;
        }

        return oldTime;
    }
    
    private int GetHighestScore(int oldScore, int newScore)
    {
        if (newScore > oldScore)
        {
            return newScore;
        }

        return oldScore;
    }
    
    private void Start()
    {
        SaveLoadManager.Load();
        
        RestartLevel();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
        
        endGameScreen.SetActive(false);
        StartTimer();

        _enemySpawner = GetComponent<WavesEnemySpawn>();
        
        PlayerController.OnPlayerDeath += OnPlayerDeathHandler;
        BossEnemy.OnDefeatBoss += OnDefeatBossHandler;
        EnemyController.OnDefeatEnemy += OnDefeatEnemyHandler;
    }
    
    private void Update()
    {
        if (_isTimerRunning && !UIController.Instance.IsGamePaused)
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
        StopGame(endGameScreen);
    }
    
    private void OnPlayerDeathHandler()
    {
        EndGame();
    }
    
    private void OnDefeatBossHandler()
    {
        if (_enemySpawner is LevelsEnemySpawn)
        {
            WinLevel();
        }
        else if (_enemySpawner is WavesEnemySpawn)
        {
            _enemySpawner.NextLevel();
        }
        OnDefeatEnemyHandler();
    }
    
    public void WinLevel()
    {
        StopGame(winLevelScreen);
    }
    
    public void AddScore(int amount)
    {
        CurrentScore += amount;
    }

    private string FormatTimer(float time)
    {
        // Format the elapsed time as minutes:seconds:milliseconds
        string minutes = Mathf.Floor(time / 60).ToString("00");
        string seconds = Mathf.Floor(time % 60).ToString("00");
        //string milliseconds = Mathf.Floor((elapsedTime * 1000) % 1000).ToString("000");

        return minutes + ":" + seconds;
    }

    private void StopGame(GameObject screenToActivate)
    {
        SaveLoadManager.Save();
        
        IsGameEnded = true;
        UIController.Instance.IsGamePaused = true;

        StopTimer();
            
        screenToActivate.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SavedSettings.highestTime = GetHighestSurvivedTime(SavedSettings.highestTime, _elapsedTime);
        SavedSettings.highestScore = GetHighestScore(SavedSettings.highestScore, CurrentScore);

        SetMaxScoreAndTime textComponents = screenToActivate.GetComponent<SetMaxScoreAndTime>();
        
        textComponents.scoreText.text = CurrentScore.ToString();
        textComponents.timeText.text = FormatTimer(_elapsedTime);
        
        SaveLoadManager.Save();
    }

    private void RestartLevel()
    {
        PlayerController.IsDead = false;
        CurrentScore = 0;

        IsGameEnded = false;
        //UIController.Instance.UnPauseGame();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        endGameScreen.SetActive(false);
        winLevelScreen.SetActive(false);
    }
    
    private void OnDefeatEnemyHandler()
    {
        CurrentScore++;
        scoreText.text = CurrentScore.ToString();
    }
}
