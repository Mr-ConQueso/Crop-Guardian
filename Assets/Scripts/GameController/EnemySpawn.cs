using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [Header("Flying Enemies")]
    [SerializeField] private GameObject[] flyingEnemyPrefabs;
    [SerializeField] private float flySpawnInterval = 2f;
    [SerializeField] private int flySpawnNumber = 1;
    [Range(0, 1)]
    [SerializeField] private float flySpawnProbability = 1f;
    [SerializeField] private float sphereRadius = 10f;
    
    [Header("Flying Boss")]
    [SerializeField] private GameObject flyBoss;
    [SerializeField] private bool spawnFlyBoss;
    [SerializeField] private int spawnFlyBossPoints;
    
    [Header("Ground Enemies")]
    [SerializeField] private GameObject[] groundEnemyPrefabs;
    [SerializeField] private float groundSpawnInterval = 2f;
    [SerializeField] private int groundSpawnNumber = 1;
    [Range(0, 1)]
    [SerializeField] private float groundSpawnProbability = 1f;
    [SerializeField] private float circleRadius = 10f;
    
    [Header("Grounded Boss")]
    [SerializeField] private GameObject groundBoss;
    [SerializeField] private bool spawnGroundBoss;
    [SerializeField] private int spawnGroundBossPoints;
    
    // ---- / Private Variables / ---- //
    private float _spawnTimer;
    private bool _hasBossSpawned;
    private GameController _gameController;

    private void Start()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        if (_gameController.GetCurrentScore() >= spawnFlyBossPoints && !_hasBossSpawned)
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
        
        if (!_hasBossSpawned)
        {
            _spawnTimer += Time.deltaTime;
            if (_spawnTimer >= flySpawnInterval && Random.value <= flySpawnProbability)
            {
                _spawnTimer = 0;

                SpawnFlyingEnemies(flySpawnNumber);
            } else if (_spawnTimer >= groundSpawnInterval && Random.value <= groundSpawnProbability)
            {
                _spawnTimer = 0;

                SpawnGroundEnemies(groundSpawnNumber);
            }
        }
    }

    private void SpawnFlyBoss()
    {
        _hasBossSpawned = true;
        Instantiate(flyBoss, GetPointOnSemiSphere(sphereRadius), Quaternion.identity);
    }
    
    private void SpawnGroundBoss()
    {
        _hasBossSpawned = true;
        Instantiate(groundBoss, GetPointOnCircle(sphereRadius), Quaternion.identity);
    }

    /// <summary>
    /// Instantiate prefabs in batches, on the
    /// surface of a sphere around the center.
    /// </summary>
    /// <param name="numberOfPrefabs"></param>
    private void SpawnFlyingEnemies(int numberOfPrefabs)
    {
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            GameObject prefabToSpawn = flyingEnemyPrefabs[Random.Range(0, flyingEnemyPrefabs.Length)];
            Instantiate(prefabToSpawn, GetPointOnSemiSphere(sphereRadius), Quaternion.identity);
        }
    }
    
    /// <summary>
    /// Instantiate prefabs in batches, on the
    /// perimeter of a circle around the player.
    /// </summary>
    /// <param name="numberOfPrefabs"></param>
    private void SpawnGroundEnemies(int numberOfPrefabs)
    {
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            GameObject prefabToSpawn = groundEnemyPrefabs[Random.Range(0, groundEnemyPrefabs.Length)];
            Instantiate(prefabToSpawn, GetPointOnCircle(circleRadius), Quaternion.identity);
        }
    }

    private Vector3 GetPointOnCircle(float radius)
    {
        Vector2 randomPoint = Random.insideUnitCircle.normalized;
        return transform.position + new Vector3(randomPoint.x, 0f, randomPoint.y) * radius;
    }

    private Vector3 GetPointOnSemiSphere(float radius)
    {
        Vector3 randomPoint;
        do
        {
            randomPoint = Random.onUnitSphere;
        } while (randomPoint.y < 0);

        return transform.position + randomPoint * radius;
    }

}
