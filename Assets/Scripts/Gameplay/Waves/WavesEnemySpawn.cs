using UnityEngine;
using Random = UnityEngine.Random;

public class WavesEnemySpawn : EnemySpawn
{
    // ---- / Serialized Variables / ---- //
    [Header("Flying Enemies")]
    [SerializeField] private GameObject[] flyingEnemyPrefabs;
    [SerializeField] private float flySpawnInterval = 2f;
    [SerializeField] private int flySpawnNumber = 1;
    [Range(0, 1)]
    [SerializeField] private float flySpawnProbability = 1f;
    [SerializeField] private float sphereRadius = 10f;
    
    [Header("Ground Enemies")]
    [SerializeField] private GameObject[] groundEnemyPrefabs;
    [SerializeField] private float groundSpawnInterval = 2f;
    [SerializeField] private int groundSpawnNumber = 1;
    [Range(0, 1)]
    [SerializeField] private float groundSpawnProbability = 1f;
    [SerializeField] private float circleRadius = 10f;
    
    [Header("Bosses")]
    [SerializeField] private GameObject groundBoss;
    [SerializeField] private GameObject flyBoss;

    [SerializeField] private int spawnBossPoints;

    private void Update()
    {
        if (GameController.GetCurrentScore() >= spawnBossPoints && !HasBossSpawned)
        {
            if (Random.value >= 0.5)
            {
                SpawnFlyBoss();
            }
            else
            {
                SpawnGroundBoss();
            }
        }
        
        if (!HasBossSpawned)
        {
            SpawnTimer += Time.deltaTime;
            if (SpawnTimer >= flySpawnInterval && Random.value <= flySpawnProbability)
            {
                SpawnTimer = 0;

                SpawnFlyingEnemies(flySpawnNumber);
            } else if (SpawnTimer >= groundSpawnInterval && Random.value <= groundSpawnProbability)
            {
                SpawnTimer = 0;

                SpawnGroundEnemies(groundSpawnNumber);
            }
        }
    }
}
