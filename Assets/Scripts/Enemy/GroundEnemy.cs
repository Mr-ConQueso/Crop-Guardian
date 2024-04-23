using UnityEngine;

namespace Enemy
{
    public class GroundEnemy : EnemyController
    {
        // ---- / Parent Variables / ---- //
        [SerializeField] protected override float MoveSpeed => 1.0f;
        [SerializeField] protected override int Health => 1;
    }
}