using Health;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    // ---- / Events / ---- //
    public delegate void DefeatEnemyEventHandler();
    public static event DefeatEnemyEventHandler OnDefeatEnemy;
    
    // ---- / Children Variables / ---- //
    [SerializeField] protected float moveSpeed = 1.0f;
    [SerializeField] protected float health = 1;
    
    // ---- / Private Variables / ---- //
    private Transform _enemyTransform;
    
    /// <summary>
    /// Remove the inputted amount of health from the enemy
    /// </summary>
    /// <param name="amountToRemove"></param>
    public void RemoveHealth(float amountToRemove)
    {
        if (health <= 0)
        {
            KillSelf();
        }
        else
        {
            health -= amountToRemove;
        }
    }
    
    /// <summary>
    /// Destroy GameObject
    /// </summary>
    public virtual void KillSelf()
    {
        OnDefeatEnemy?.Invoke();
        Destroy(gameObject);
    }
    
    private void Start()
    {
        _enemyTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (!GameController.Instance.IsGamePaused)
        {
            Move();
        }
        else
        {
            StopMoving();
        }
    }

    protected virtual void Move()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _enemyTransform.position, step);
    }
    
    protected virtual void StopMoving() { }
}
