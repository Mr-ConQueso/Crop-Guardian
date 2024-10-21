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
    
    // ---- / Events / ---- //
    #region Game Loop Events
    public delegate void GameStartEventHandler();
    public static event GameStartEventHandler OnGameStart;
    
    public delegate void GameEndEventHandler();
    public static event GameEndEventHandler OnGameEnd;
    
    public delegate void GamePausedEventHandler();
    public static event GamePausedEventHandler OnGamePaused;
    public delegate void GameResumedEventHandler();
    public static event GameResumedEventHandler OnGameResumed;
    
    #endregion
    
    // ---- / Hidden Public Variables / ---- //
    [HideInInspector] public bool CanPauseGame = false;
    [HideInInspector] public bool IsPlayerFrozen { get; private set; } = true;
    [HideInInspector] public bool IsGamePaused { get; private set; }
    [HideInInspector] public int CurrentScore { get; private set; }
    [HideInInspector] public float TimerValue { get; private set; }
    
    // ---- / Serialized Variables / ---- //
    [Header("In-Game Info")]
    [SerializeField] private TMP_Text survivedTimeText;
    
    [Header("Volumes")]
    [SerializeField] private Volume globalVolume;
    [SerializeField] private Volume menusVolume;
    
    // ---- / Private Variables / ---- //
    private BaseEnemySpawn _enemySpawner;
    private bool _isTimerRunning;
    private bool _isGameEnded;

    public void SwitchPostProcessVolume(bool isMenusVolumeActive)
    {
        menusVolume.enabled = isMenusVolumeActive;
        globalVolume.enabled = !isMenusVolumeActive;
    }
    
    public void InvokeOnGameResumed()
    {
        IsPlayerFrozen = false;
        IsGamePaused = false;
        OnGameResumed?.Invoke();
    }
    public void InvokeOnWinGame()
    {
        WinGame();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        PlayerController.OnPlayerDeath += OnPlayerDeath;
        BossEnemy.OnDefeatBoss += OnDefeatBoss;
        EnemyController.OnDefeatEnemy += OnDefeatEnemy;
    }

    private void OnDestroy()
    {
        PlayerController.OnPlayerDeath -= OnPlayerDeath;
        BossEnemy.OnDefeatBoss -= OnDefeatBoss;
        EnemyController.OnDefeatEnemy -= OnDefeatEnemy;
    }

    private void StartTimer()
    {
        _isTimerRunning = true;
    }

    private void StopTimer()
    {
        _isTimerRunning = false;
    }
    
    private void Start()
    {
        SaveLoadManager.Load();
        
        RestartLevel();
        StartGame();
        StartTimer();

        _enemySpawner = GetComponent<WavesEnemySpawn>();
    }
    
    private void Update()
    {
        if (_isTimerRunning && !IsGamePaused)
        {
            TimerValue += Time.deltaTime;
            survivedTimeText.text = HelperFunctions.FormatTimer(TimerValue);
        }
        
        if (InputManager.WasEscapePressed && CanPauseGame)
        {
            IsGamePaused = !IsGamePaused;
            if (IsGamePaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }
    
    private void StartGame()
    {
        IsPlayerFrozen = false;
        IsGamePaused = false;
        CanPauseGame = true;
        OnGameStart?.Invoke();
    }

    private void PauseGame()
    {
        IsPlayerFrozen = true;
        HelperFunctions.SetCursorMode(CursorLockMode.None, true);
        OnGamePaused?.Invoke();
    }
    
    private void ResumeGame()
    {
        IsPlayerFrozen = false;
        HelperFunctions.SetCursorMode(CursorLockMode.Locked, false);
        OnGameResumed?.Invoke();
    }
    
    private void OnPlayerDeath()
    {
        LoseGame();
    }
    
    private void OnDefeatBoss()
    {
        if (_enemySpawner is LevelsEnemySpawn)
        {
            WinGame();
        }
        else if (_enemySpawner is WavesEnemySpawn)
        {
            _enemySpawner.NextLevel();
        }
        OnDefeatEnemy();
    }

    private void WinGame()
    {
        MenuManager.OpenMenu(Menu.WinMenu);
        EndGame();
    }

    private void LoseGame()
    {
        MenuManager.OpenMenu(Menu.LoseMenu);
        EndGame();
    }
    
    private void EndGame()
    {
        if (!_isGameEnded)
        {
            SaveLoadManager.Save();
            IsPlayerFrozen = true;
            
            StopTimer();
        
            HelperFunctions.SetCursorMode(CursorLockMode.None, true);
        
            SaveLoadManager.Save();
            
            OnGameEnd?.Invoke();

            IsGamePaused = true;
            _isGameEnded = true;
        }
    }

    private void RestartLevel()
    {
        PlayerController.IsDead = false;
        CurrentScore = 0;
        
        HelperFunctions.SetCursorMode(CursorLockMode.Locked, false);
    }
    
    private void OnDefeatEnemy()
    {
        CurrentScore++;
    }
}
