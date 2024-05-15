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

    protected virtual void Update()
    {
        DrawDebugLines(CalculateMaxShootingHeight());
    }

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
        RaycastHit hit;
        Vector3 randomPoint;
        do
        {
            randomPoint = new Vector3(Random.Range(-radius, radius), 200, Random.Range(-radius, radius)) + transform.position;
        } while (!Physics.Raycast(randomPoint, Vector3.down, out hit, Mathf.Infinity, groundLayerMask) ||
                 Vector3.Distance(randomPoint, transform.position) < 0.6f * radius);

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
    
    private void DrawDebugLines(float maxY)
    {
        Vector3 playerStart = transform.position;
        
        // Draw line representing shooting height limit
        Vector3 shootingHeightLimitEnd = Vector3.up * maxY;
        Debug.DrawLine(playerStart, shootingHeightLimitEnd, Color.blue);
        
        // Draw horizontal height limit
        Vector3 horizontalEnd = shootingHeightLimitEnd + Vector3.forward * 100;
        Debug.DrawLine(shootingHeightLimitEnd, horizontalEnd, Color.magenta);
    }
}
