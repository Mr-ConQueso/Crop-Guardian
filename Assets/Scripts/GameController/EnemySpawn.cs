using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [SerializeField] private GameObject groundEnemyPrefab;
    [SerializeField] private GameObject flyingEnemyPrefab;
    [SerializeField] private float spawnDelay = 2f;
    [SerializeField] private float sphereRadius = 10f;
    [SerializeField] private float groundDistance = 10f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnFlyingEnemies), 0f, spawnDelay);
        InvokeRepeating(nameof(SpawnGroundEnemies), 0f, spawnDelay);
    }

    public void SpawnFlyingEnemies()
    {
        Vector3 randomDirection = Random.onUnitSphere;
        Vector3 spawnPosition = randomDirection * sphereRadius + transform.position;
        GameObject enemy = Instantiate(flyingEnemyPrefab, spawnPosition, Quaternion.identity);
        
        // Orient the enemy to "sit" on the semi-sphere
        enemy.transform.up = randomDirection;
        /*
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 randomDirection = UnityEngine.Random.onUnitSphere;
            Vector3 spawnPosition = randomDirection * sphereRadius + transform.position;
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            
            // Orient the enemy to "sit" on the semi-sphere
            enemy.transform.up = randomDirection;
        }
        */
    }
    
    public void SpawnGroundEnemies()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        
        float x = Mathf.Cos(angle) * sphereRadius;
        float z = Mathf.Sin(angle) * sphereRadius;
        Vector3 spawnPosition = new Vector3(x, groundDistance, z) + transform.position;

        Instantiate(groundEnemyPrefab, spawnPosition, Quaternion.identity);
    }
}
