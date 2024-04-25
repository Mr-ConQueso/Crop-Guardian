using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // ---- / Children Variables / ---- //
    protected virtual float MoveSpeed { get; set; } = 1.0f;
    protected virtual int Health { get; set; } = 1;
    
    // ---- / Private Variables / ---- //
    private Transform _enemyTransform;
    
    /// <summary>
    /// Remove the inputted amount of health from the enemy
    /// </summary>
    /// <param name="amountToRemove"></param>
    public void RemoveHealth(int amountToRemove)
    {
        if (Health > 1)
        {
            Health -= amountToRemove;
        }
        else
        {
            KillSelf();
        }
    }
    
    /// <summary>
    /// Destroy GameObject
    /// </summary>
    protected virtual void KillSelf()
    {
        Destroy(gameObject);
    }
    
    private void Start()
    {
        _enemyTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        float step = MoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _enemyTransform.position, step);
    }
}
