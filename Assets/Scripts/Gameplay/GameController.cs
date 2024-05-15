using System;
using Enemy;
using SaveLoad;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class GameController : MonoBehaviour, ISaveable
{
    // ---- / Singleton / ---- //
    public static GameController Instance;
    
    // ---- / Static Variables / ---- //
    public static bool IsGameEnded;
    
    public float highestSurviveTime;
    public int CurrentScore { get; private set; }
    public int highScore;
    
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
    
    private void UpdateSurvivedTime(float newScore)
    {
        if (newScore > highestSurviveTime)
        {
            highestSurviveTime = newScore;
        }
    }
    
    private void UpdateHighScore(int newScore)
    {
        if (newScore > highScore)
        {
            highScore = newScore;
        }
    }
    
    private void Start()
    {
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
        IsGameEnded = true;

        StopTimer();
            
        screenToActivate.SetActive(true);
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        UpdateSurvivedTime(_elapsedTime);
        UpdateHighScore(CurrentScore);

        SetMaxScoreAndTime textComponents = screenToActivate.GetComponent<SetMaxScoreAndTime>();
        
        textComponents.scoreText.text = FormatTimer(highestSurviveTime);
        textComponents.timeText.text = highScore.ToString();
    }

    private void RestartLevel()
    {
        PlayerController._isDead = false;
        CurrentScore = 0;

        IsGameEnded = false;
        UIController.Instance.UnPauseGame();
        
        endGameScreen.SetActive(false);
        winLevelScreen.SetActive(false);
    }
    
    private void OnDefeatEnemyHandler()
    {
        CurrentScore++;
        scoreText.text = CurrentScore.ToString();
    }

    public object CaptureState()
    {
        return new SaveData()
        {
            hightestScore = highScore,
            score = CurrentScore,
            highestTime = highestSurviveTime
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        highScore = saveData.hightestScore;
        CurrentScore = saveData.score;
        highestSurviveTime = saveData.highestTime;
    }
    
    [Serializable]
    private struct SaveData
    {
        public int hightestScore;
        public int score;
        public float highestTime;
    }
}
