using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : BaseEnemySpawn
{
    // ---- / Serialized Variables / ---- //
    [Header("Flying Enemies")]
    [SerializeField] GameObject[] flyingEnemyPrefabs;
    [SerializeField] float flySpawnInterval = 2f;
    [SerializeField] int flySpawnNumber = 1;
    [Range(0, 1)]
    [SerializeField] float flySpawnProbability = 1f;
    [SerializeField] float sphereRadius = 10f;
    
    [Header("Ground Enemies")]
    [SerializeField] GameObject[] groundEnemyPrefabs;
    [SerializeField] float groundSpawnInterval = 2f;
    [SerializeField] int groundSpawnNumber = 1;
    [Range(0, 1)]
    [SerializeField] float groundSpawnProbability = 1f;
    [SerializeField] float circleRadius = 10f;
    
    [Header("Bosses")]
    [SerializeField] GameObject groundBoss;
    [SerializeField] GameObject flyBoss;

    [SerializeField] int spawnBossPoints;
    
    // ---- / Private Variables / ---- //
    [SerializeField] private bool spawnBoss;
    private GameController _gameController;

    private void Start()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        if (_gameController.GetCurrentScore() >= spawnBossPoints && !HasBossSpawned && spawnBoss)
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
        if (_gameController.GetCurrentScore() > spawnBossPoints && !spawnBoss)
        {
            _gameController.WinLevel();
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

                SpawnGroundEnemies(groundSpawnNumber, groundEnemyPrefabs);
            }
        }
    }
}
