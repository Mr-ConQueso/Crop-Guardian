using UnityEngine;
using Random = UnityEngine.Random;

public class WavesEnemySpawn : BaseEnemySpawn
{
    // ---- / Serialized Variables / ---- //
    [Header("Flying Enemies")]
    [SerializeField] GameObject[] flyingEnemyPrefabs;
    [SerializeField] float flySpawnInterval = 2f;
    [SerializeField] int flySpawnNumber = 1;
    [Range(0, 1)]
    [SerializeField] float flySpawnProbability = 1f;
    
    [Header("Ground Enemies")]
    [SerializeField] GameObject[] groundEnemyPrefabs;
    [SerializeField] float groundSpawnInterval = 2f;
    [Range(0, 1)]
    [SerializeField] float groundSpawnProbability = 1f;
    [SerializeField] float circleRadius = 10f;
    
    [Header("Bosses")]
    [SerializeField] GameObject groundBoss;
    [SerializeField] GameObject flyBoss;

    [SerializeField] int spawnBossPoints;
    
    // ---- / Private Variables / ---- //
    private GameController _gameController;
    
    private void Start()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        if (_gameController.GetCurrentScore() >= spawnBossPoints && !HasBossSpawned)
        {
            if (Random.value >= 0.5)
            {
                SpawnFlyBoss(flyBoss);
            }
            else
            {
                SpawnGroundBoss(groundBoss);
            }
        }
        
        if (!HasBossSpawned)
        {
            SpawnTimer += Time.deltaTime;
            if (SpawnTimer >= flySpawnInterval && Random.value <= flySpawnProbability)
            {
                SpawnTimer = 0;

                SpawnFlyingEnemies(flySpawnNumber, flyingEnemyPrefabs);
            } else if (SpawnTimer >= groundSpawnInterval && Random.value <= groundSpawnProbability)
            {
                SpawnTimer = 0;

                SpawnGroundEnemies(GroundSpawnNumber, groundEnemyPrefabs);
            }
        }
    }
}
