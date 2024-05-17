using UnityEngine;
using Random = UnityEngine.Random;

public class BaseEnemySpawn : MonoBehaviour
{
    // ---- / Parent Variables / ---- //
    protected float SpawnFlyTimer;
    protected float SpawnGroundTimer = 1.5f;
    protected bool HasBossSpawned;
    protected GameController GameController;
    
    // ---- / Serialized Variables / ---- //
    [Header("Flying Enemies")]
    [SerializeField] protected GameObject[] flyingEnemyPrefabs;
    [SerializeField] protected float flySpawnInterval = 2f;
    [SerializeField] protected int flySpawnNumber = 1;
    [Range(0, 1), SerializeField] protected float flySpawnProbability = 1f;
    [SerializeField] protected float sphereRadius = 10f;
    
    [Header("Ground Enemies")]
    [SerializeField] protected GameObject[] groundEnemyPrefabs;
    [SerializeField] protected float groundSpawnInterval = 2f;
    [SerializeField] protected int groundSpawnNumber = 1;
    [Range(0, 1), SerializeField] protected float groundSpawnProbability = 1f;
    [SerializeField] protected float circleRadius = 10f;
    [SerializeField] protected LayerMask groundLayerMask;
    
    [Header("Bosses")]
    [SerializeField] protected GameObject groundBoss;
    [SerializeField] protected GameObject flyBoss;

    [SerializeField] protected int spawnBossPoints = 5;

    protected virtual void SpawnFlyBoss(GameObject boss, float spawnSphereRadius)
    {
        HasBossSpawned = true;
        Instantiate(boss, GetPointOnSemiSphere(spawnSphereRadius), Quaternion.identity);
    }
    
    protected virtual void SpawnGroundBoss(GameObject boss, float spawnCircleRadius)
    {
        HasBossSpawned = true;
        Instantiate(boss, GetPointOnCircle(spawnCircleRadius), Quaternion.identity);
    }

    /// <summary>
    /// Instantiate prefabs in batches, on the
    /// surface of a sphere around the center.
    /// </summary>
    /// <param name="numberOfPrefabs"></param>
    /// <param name="flyingPrefabs"></param>
    /// <param name="spawnSphereRadius"></param>
    protected virtual void SpawnFlyingEnemies(int numberOfPrefabs, GameObject[] flyingPrefabs, float spawnSphereRadius)
    {
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            GameObject prefabToSpawn = flyingPrefabs[Random.Range(0, flyingPrefabs.Length)];
            Instantiate(prefabToSpawn, GetPointOnSemiSphere(spawnSphereRadius), Quaternion.identity);
        }
    }

    /// <summary>
    /// Instantiate prefabs in batches, on the
    /// perimeter of a circle around the player.
    /// </summary>
    /// <param name="numberOfPrefabs"></param>
    /// <param name="groundPrefabs"></param>
    /// <param name="spawnCircleRadius"></param>
    protected virtual void SpawnGroundEnemies(int numberOfPrefabs, GameObject[] groundPrefabs, float spawnCircleRadius)
    {
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            GameObject prefabToSpawn = groundPrefabs[Random.Range(0, groundPrefabs.Length)];
            Instantiate(prefabToSpawn, GetPointOnCircle(spawnCircleRadius), Quaternion.identity);
        }
    }

    /// <summary>
    /// Generate a point on a virtual circle
    /// </summary>
    /// <param name="radius"></param>
    /// <returns></returns>
    protected virtual Vector3 GetPointOnCircle(float radius)
    {
        RaycastHit hit;
        Vector3 randomPoint;
        do
        {
            randomPoint = new Vector3(Random.Range(-radius, radius), 200, Random.Range(-radius, radius)) + transform.position;
        } while (!Physics.Raycast(randomPoint, Vector3.down, out hit, Mathf.Infinity, groundLayerMask) ||
                 Vector3.Distance(randomPoint, transform.position) < 0.85f * radius);

        return hit.point;
    }



    /// <summary>
    /// Generate a point on a virtual sphere, in the top half
    /// </summary>
    /// <param name="radius"></param>
    /// <returns></returns>
    protected virtual Vector3 GetPointOnSemiSphere(float radius)
    {
        float maxY = CalculateMaxShootingHeight();

        Vector3 randomPoint;
        do
        {
            randomPoint = Random.onUnitSphere;

            randomPoint *= radius;

            randomPoint += transform.position;
        } while (randomPoint.y < transform.position.y || randomPoint.y > maxY);

        return randomPoint;
    }


    private float CalculateMaxShootingHeight()
    {
        float maxY = transform.position.y + (sphereRadius * 0.866f);
        return maxY;
    }
    
    public virtual void NextLevel() { }
}
