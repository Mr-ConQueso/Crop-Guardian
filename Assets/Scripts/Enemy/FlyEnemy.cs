using UnityEngine;

namespace Enemy
{
    public class FlyEnemy : EnemyController
    {
        // ---- / Parent Variables / ---- //
        [SerializeField] protected override float MoveSpeed => 1.2f;
        [SerializeField] protected override int Health => 1;
    }
}