using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [Header("Flying Enemies")]
    [SerializeField] private GameObject[] flyingEnemyPrefabs;
    [SerializeField] private float flySpawnInterval = 2f;
    [Range(0, 1)]
    [SerializeField] private float flySpawnProbability = 1f;
    [SerializeField] private float sphereRadius = 10f;
    
    [Header("Ground Enemies")]
    [SerializeField] private GameObject[] groundEnemyPrefabs;
    [SerializeField] private float groundSpawnInterval = 2f;
    [Range(0, 1)]
    [SerializeField] private float groundSpawnProbability = 1f;
    [SerializeField] private float circleRadius = 10f;
    
    // ---- / Private Variables / ---- //
    private float _spawnTimer;
    
    private void Update()
    {
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= flySpawnInterval && Random.value <= flySpawnProbability)
        {
            _spawnTimer = 0;

            SpawnFlyingEnemies(1);
        } else if (_spawnTimer >= groundSpawnInterval && Random.value <= groundSpawnProbability)
        {
            _spawnTimer = 0;

            SpawnGroundEnemies(1);
        }
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
            // Get a random point on the unit sphere
            Vector3 randomPoint;
            do
            {
                randomPoint = Random.onUnitSphere;
            } while (randomPoint.y < 0); // Ensure the random point is above y = 0

            // Scale the random point by the sphere radius
            Vector3 position = transform.position + randomPoint * sphereRadius;

            // Instantiate the prefab at the calculated position
            GameObject prefabToSpawn = flyingEnemyPrefabs[Random.Range(0, flyingEnemyPrefabs.Length)];
            Instantiate(prefabToSpawn, position, Quaternion.identity);
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
            // Get a random point inside or on the unit circle
            // Vector2 randomPoint = RandomOnUnitCircle(circleRadius);
            Vector2 randomPoint = Random.insideUnitCircle.normalized;

            // Scale the random point by the circle radius
            Vector3 position = transform.position + new Vector3(randomPoint.x, 0f, randomPoint.y) * sphereRadius;

            // Instantiate the prefab at the calculated position
            GameObject prefabToSpawn = groundEnemyPrefabs[Random.Range(0, groundEnemyPrefabs.Length)];
            Instantiate(prefabToSpawn, position, Quaternion.identity);
        }
    }

    private Vector3 RandomOnUnitCircle(float radius)
    {
        var angle = Random.value * (2f * Mathf.PI);
        var direction = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));
        
        return direction * radius;
    }

}
