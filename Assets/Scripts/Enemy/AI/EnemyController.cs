using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // ---- / Events / ---- //
    public delegate void DefeatEnemyEventHandler();
    public static event DefeatEnemyEventHandler OnDefeatEnemy;
    
    // ---- / Children Variables / ---- //
    [SerializeField] protected float MoveSpeed = 1.0f;
    [SerializeField] protected int Health = 1;
    
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
        OnDefeatEnemy?.Invoke();
        Destroy(gameObject);
    }
    
    private void Start()
    {
        _enemyTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (!UIController.Instance.IsGamePaused)
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
        float step = MoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _enemyTransform.position, step);
    }
    
    protected virtual void StopMoving() { }
}
