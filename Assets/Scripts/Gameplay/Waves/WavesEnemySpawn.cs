using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class WavesEnemySpawn : BaseEnemySpawn
{
    // ---- / Public Variables / ---- //
    public int CurrentWaveScore { get; private set; }
    
    // ---- / Serialized Variables / ---- //
    [SerializeField] private GameObject wavesScreen;
    [SerializeField] private TMP_Text wavesScreenText;

    // ---- / Private Variables / ---- //   
    private int _currentWave = 1;

    private void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        EnemyController.OnDefeatEnemy += OnDefeatEnemyHandler;
    }

    private void OnDestroy()
    {
        EnemyController.OnDefeatEnemy -= OnDefeatEnemyHandler;
    }

    private void Update()
    {
        Waves();
    }

    private void Waves()
    {
        if (CurrentWaveScore >= spawnBossPoints && !HasBossSpawned)
        {
            if (Random.value >= 0.5)
            {
                SpawnFlyBoss(flyBoss, sphereRadius);
            }
            else
            {
                SpawnGroundBoss(groundBoss, circleRadius);
            }
        }
        
        if (!HasBossSpawned)
        {
            SpawnTimer += Time.deltaTime;
            if (SpawnTimer >= flySpawnInterval && Random.value <= flySpawnProbability)
            {
                SpawnTimer = 0;

                SpawnFlyingEnemies(flySpawnNumber, flyingEnemyPrefabs, sphereRadius);
            } else if (SpawnTimer >= groundSpawnInterval && Random.value <= groundSpawnProbability)
            {
                SpawnTimer = 0;

                SpawnGroundEnemies(groundSpawnNumber, groundEnemyPrefabs, circleRadius);
            }
        }
    }

    public override void NextLevel()
    {
        HasBossSpawned = false;
        CurrentWaveScore = 0;
        _currentWave++;
        wavesScreen.SetActive(true);
        wavesScreenText.text = "Wave : " + _currentWave;
        Invoke(nameof(HideWaveMenu), 1.35f);
        AddWaveDifficulty();
    }

    private void AddWaveDifficulty()
    {
        flySpawnInterval -= 0.1f;
        flySpawnNumber += 1;
        flySpawnProbability += 0.1f;
        
        groundSpawnInterval -= 0.1f;
        groundSpawnNumber += 1;
        groundSpawnProbability += 0.1f;

        spawnBossPoints += 1;
    }

    private void HideWaveMenu()
    {
        wavesScreen.SetActive(false);
    }
    
    private void OnDefeatEnemyHandler()
    {
        CurrentWaveScore++;
    }
}
