using UnityEngine;

namespace Enemy
{
    public class FlyEnemy : EnemyController
    {
        // ---- / Parent Variables / ---- //
        [SerializeField] float moveSpeed = 1.2f;
        [SerializeField] int health = 1;
    }
}