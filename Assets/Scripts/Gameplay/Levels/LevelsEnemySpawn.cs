using UnityEngine;
using Random = UnityEngine.Random;

public class LevelsEnemySpawn : BaseEnemySpawn
{
    // ---- / Private Variables / ---- //
    private bool _spawnBoss;

    private void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        SingleLevel();
    }

    private void SingleLevel()
    {
        if (GameController.Instance.CurrentScore >= spawnBossPoints && !HasBossSpawned && _spawnBoss)
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
        if (GameController.Instance.CurrentScore > spawnBossPoints && !_spawnBoss)
        {
            GameController.WinLevel();
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
}
