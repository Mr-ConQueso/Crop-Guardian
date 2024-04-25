using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    // ---- / Parent Variables / ---- //
    protected float SpawnTimer;
    protected bool HasBossSpawned;
    protected GameController GameController;
    
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
    [SerializeField] private bool spawnBoss;

    [SerializeField] private GameObject groundBoss;
    [SerializeField] private GameObject flyBoss;

    [SerializeField] private int spawnBossPoints;

    private void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        if (GameController.GetCurrentScore() >= spawnBossPoints && !HasBossSpawned && spawnBoss)
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
        if (GameController.GetCurrentScore() > spawnBossPoints && !spawnBoss)
        {
            GameController.WinLevel();
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

    protected virtual  void SpawnFlyBoss()
    {
        HasBossSpawned = true;
        Instantiate(flyBoss, GetPointOnSemiSphere(sphereRadius), Quaternion.identity);
    }
    
    protected virtual  void SpawnGroundBoss()
    {
        HasBossSpawned = true;
        Instantiate(groundBoss, GetPointOnCircle(sphereRadius), Quaternion.identity);
    }

    /// <summary>
    /// Instantiate prefabs in batches, on the
    /// surface of a sphere around the center.
    /// </summary>
    /// <param name="numberOfPrefabs"></param>
    protected virtual  void SpawnFlyingEnemies(int numberOfPrefabs)
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
    protected virtual  void SpawnGroundEnemies(int numberOfPrefabs)
    {
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            GameObject prefabToSpawn = groundEnemyPrefabs[Random.Range(0, groundEnemyPrefabs.Length)];
            Instantiate(prefabToSpawn, GetPointOnCircle(circleRadius), Quaternion.identity);
        }
    }

    /// <summary>
    /// Generate a point on a virtual circle
    /// </summary>
    /// <param name="radius"></param>
    /// <returns></returns>
    protected virtual  Vector3 GetPointOnCircle(float radius)
    {
        Vector2 randomPoint = Random.insideUnitCircle.normalized;
        return transform.position + new Vector3(randomPoint.x, 0f, randomPoint.y) * radius;
    }

    /// <summary>
    /// Generate a point on a virtual sphere, in the top half
    /// </summary>
    /// <param name="radius"></param>
    /// <returns></returns>
    protected virtual Vector3 GetPointOnSemiSphere(float radius)
    {
        Vector3 randomPoint;
        do
        {
            randomPoint = Random.onUnitSphere;
        } while (randomPoint.y < 0);

        return transform.position + randomPoint * radius;
    }

}
