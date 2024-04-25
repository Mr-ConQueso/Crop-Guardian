using UnityEngine;

namespace Enemy
{
    public class BossEnemy : EnemyController
    {
        // ---- / Parent Variables / ---- //
        [SerializeField] float moveSpeed = 1.5f;
        [SerializeField] int health = 3;
        
        // ---- / Events / ---- //
        public delegate void WinGameEventHandler();
        public static event WinGameEventHandler OnWinGame;
        
        protected override void KillSelf()
        {
            OnWinGame?.Invoke();
            Destroy(gameObject);
        }
    }
}