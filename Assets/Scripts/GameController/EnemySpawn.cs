using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [SerializeField] private GameObject[] groundEnemyPrefabs;
    [SerializeField] private GameObject[] flyingEnemyPrefabs;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float sphereRadius = 10f;
    [SerializeField] private float circleRadius = 10f;
    
    // ---- / Private Variables / ---- //
    private float _spawnTimer;
    
    private void Update()
    {
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= spawnInterval)
        {
            _spawnTimer = 0; // Reset the timer

            // Call the spawning function
            SpawnFlyingEnemies(1);
            SpawnGroundEnemies(1);
        }
    }
    
    private void SpawnFlyingEnemies(int numberOfPrefabs)
    {
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            // Calculate spherical coordinates
            float inclination = Random.Range(0f, Mathf.PI / 2f); // angle from the top
            float azimuth = Random.Range(0f, Mathf.PI * 2f); // angle around the circle

            // Convert spherical coordinates to Cartesian coordinates
            float x = sphereRadius * Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float y = sphereRadius * Mathf.Cos(inclination);
            float z = sphereRadius * Mathf.Sin(inclination) * Mathf.Sin(azimuth);

            // Instantiate prefab at the calculated position
            Vector3 position = transform.position + new Vector3(x, y, z);
            GameObject prefabToSpawn = flyingEnemyPrefabs[Random.Range(0, flyingEnemyPrefabs.Length)];
            Instantiate(prefabToSpawn, position, Quaternion.identity, transform);
        }
    }

    private void SpawnGroundEnemies(int numberOfPrefabs)
    {
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            // Calculate the angle for this prefab around the circle
            float angle = i * Mathf.PI * 2f / numberOfPrefabs;

            // Calculate the position using trigonometry
            float x = Mathf.Cos(angle) * circleRadius;
            float z = Mathf.Sin(angle) * circleRadius;
            Vector3 position = transform.position + new Vector3(x, 0f, z);

            // Instantiate the prefab at the calculated position
            GameObject prefabToSpawn = groundEnemyPrefabs[Random.Range(0, groundEnemyPrefabs.Length)];
            Instantiate(prefabToSpawn, position, Quaternion.identity, transform);
        }
    }
}
