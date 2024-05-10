using UnityEngine;
using Random = UnityEngine.Random;

public class BaseEnemySpawn : MonoBehaviour
{
    // ---- / Parent Variables / ---- //
    protected float SpawnTimer;
    protected bool HasBossSpawned;
    protected GameController GameController;
    
    // ---- / Serialized Variables / ---- //
    [Header("Flying Enemies")]
    [SerializeField] protected GameObject[] flyingEnemyPrefabs;
    [SerializeField] protected float flySpawnInterval = 2f;
    [SerializeField] protected int flySpawnNumber = 1;
    [Range(0, 1)]
    [SerializeField] protected float flySpawnProbability = 1f;
    [SerializeField] protected float circleRadius = 10f;
    
    [Header("Ground Enemies")]
    [SerializeField] protected GameObject[] groundEnemyPrefabs;
    [SerializeField] protected float groundSpawnInterval = 2f;
    [SerializeField] protected int groundSpawnNumber = 1;
    [Range(0, 1)]
    [SerializeField] protected float groundSpawnProbability = 1f;
    [SerializeField] protected float sphereRadius = 10f;
    [SerializeField] protected float sphereMinHeight = 35f;
    
    [Header("Bosses")]
    [SerializeField] protected GameObject groundBoss;
    [SerializeField] protected GameObject flyBoss;

    [SerializeField] protected int spawnBossPoints;

    protected virtual void SpawnFlyBoss(GameObject boss, float sphereRadius)
    {
        HasBossSpawned = true;
        Instantiate(boss, GetPointOnSemiSphere(sphereRadius), Quaternion.identity);
    }
    
    protected virtual void SpawnGroundBoss(GameObject boss, float circleRadius)
    {
        HasBossSpawned = true;
        Instantiate(boss, GetPointOnCircle(circleRadius), Quaternion.identity);
    }

    /// <summary>
    /// Instantiate prefabs in batches, on the
    /// surface of a sphere around the center.
    /// </summary>
    /// <param name="numberOfPrefabs"></param>
    /// <param name="flyingEnemyPrefabs"></param>
    protected virtual void SpawnFlyingEnemies(int numberOfPrefabs, GameObject[] flyingEnemyPrefabs, float sphereRadius)
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
    /// <param name="groundEnemyPrefabs"></param>
    protected virtual void SpawnGroundEnemies(int numberOfPrefabs, GameObject[] groundEnemyPrefabs, float circleRadius)
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
    protected virtual Vector3 GetPointOnCircle(float radius)
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
        } while (randomPoint.y < sphereMinHeight || randomPoint.y > GetMaxSpawnHeight());

        return transform.position + randomPoint * radius;
    }
    
    public virtual void NextLevel() { }

    protected float GetMaxSpawnHeight()
    {
        float playerHeight = 2.2f;
        float sphereImpactWidth = playerHeight * Mathf.Tan(30);

        float halfHeight = sphereImpactWidth * Mathf.Tan(30);
        
        Debug.Log(playerHeight + halfHeight);
        return playerHeight + halfHeight;
    }
}
