using UnityEngine;
using Random = UnityEngine.Random;

public class BaseEnemySpawn : MonoBehaviour
{
    // ---- / Parent Variables / ---- //
    protected float SpawnTimer;
    protected bool HasBossSpawned;
    protected float SphereRadius = 10f;
    protected int GroundSpawnNumber = 1;
    protected float CircleRadius = 10f;
    protected GameObject GroundBoss;
    protected GameObject FlyBoss;
    
    // ---- / Private Variables / ---- //
    private bool _spawnBoss;

    protected virtual void SpawnFlyBoss()
    {
        HasBossSpawned = true;
        Instantiate(FlyBoss, GetPointOnSemiSphere(SphereRadius), Quaternion.identity);
    }
    
    protected virtual void SpawnGroundBoss()
    {
        HasBossSpawned = true;
        Instantiate(GroundBoss, GetPointOnCircle(SphereRadius), Quaternion.identity);
    }

    /// <summary>
    /// Instantiate prefabs in batches, on the
    /// surface of a sphere around the center.
    /// </summary>
    /// <param name="numberOfPrefabs"></param>
    /// <param name="flyingEnemyPrefabs"></param>
    protected virtual void SpawnFlyingEnemies(int numberOfPrefabs, GameObject[] flyingEnemyPrefabs)
    {
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            GameObject prefabToSpawn = flyingEnemyPrefabs[Random.Range(0, flyingEnemyPrefabs.Length)];
            Instantiate(prefabToSpawn, GetPointOnSemiSphere(SphereRadius), Quaternion.identity);
        }
    }

    /// <summary>
    /// Instantiate prefabs in batches, on the
    /// perimeter of a circle around the player.
    /// </summary>
    /// <param name="numberOfPrefabs"></param>
    /// <param name="groundEnemyPrefabs"></param>
    protected virtual void SpawnGroundEnemies(int numberOfPrefabs, GameObject[] groundEnemyPrefabs)
    {
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            GameObject prefabToSpawn = groundEnemyPrefabs[Random.Range(0, groundEnemyPrefabs.Length)];
            Instantiate(prefabToSpawn, GetPointOnCircle(CircleRadius), Quaternion.identity);
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
        } while (randomPoint.y < 0);

        return transform.position + randomPoint * radius;
    }

}
