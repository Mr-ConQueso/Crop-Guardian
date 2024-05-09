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
        if (GameController.GetCurrentScore() >= spawnBossPoints && !HasBossSpawned && _spawnBoss)
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
        if (GameController.GetCurrentScore() > spawnBossPoints && !_spawnBoss)
        {
            GameController.WinLevel();
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
}
