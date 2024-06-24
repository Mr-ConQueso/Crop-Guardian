using System;
using SaveLoad;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class WavesEnemySpawn : BaseEnemySpawn, ISaveable
{
    // ---- / Serialized Variables / ---- //
    [SerializeField] private GameObject wavesScreen;
    [SerializeField] private TMP_Text wavesScreenText;

    // ---- / Private Variables / ---- //   
    private int _currentWave = 1;
    private int _highestWave;
    private int _currentWaveScore;

    private void Start()
    {
        EnemyController.OnDefeatEnemy += OnDefeatEnemyHandler;
    }

    private void OnDestroy()
    {
        EnemyController.OnDefeatEnemy -= OnDefeatEnemyHandler;
    }

    private void Update()
    {
        if (!UIController.Instance.IsGamePaused)
        {
            Waves();
        }
    }

    private void Waves()
    {
        if (_currentWaveScore >= spawnBossPoints && !HasBossSpawned)
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
            SpawnFlyTimer += Time.deltaTime;
            SpawnGroundTimer += Time.deltaTime;
            if (SpawnFlyTimer >= flySpawnInterval && Random.value <= flySpawnProbability)
            {
                SpawnFlyTimer = 0;

                SpawnFlyingEnemies(flySpawnNumber, flyingEnemyPrefabs, sphereRadius);
            }
            if (SpawnGroundTimer >= groundSpawnInterval && Random.value <= groundSpawnProbability)
            {
                SpawnGroundTimer = 0;

                SpawnGroundEnemies(groundSpawnNumber, groundEnemyPrefabs, circleRadius);
            }
        }
    }

    public override void NextLevel()
    {
        HasBossSpawned = false;
        _currentWaveScore = 0;
        
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
    }

    private void HideWaveMenu()
    {
        wavesScreen.SetActive(false);
    }
    
    private void OnDefeatEnemyHandler()
    {
        _currentWaveScore++;
    }

    public object CaptureState()
    {
        return new SaveData()
        {
            highestWave = _highestWave,
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        _highestWave = saveData.highestWave;
    }
    
    [Serializable]
    private struct SaveData
    {
        public int highestWave;
    }
}
