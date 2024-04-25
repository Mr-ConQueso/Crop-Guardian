using UnityEngine;

namespace Enemy
{
    public class GroundEnemy : EnemyController
    {
        // ---- / Parent Variables / ---- //
        [SerializeField] float moveSpeed = 1.0f;
        [SerializeField] int health = 1;
    }
}