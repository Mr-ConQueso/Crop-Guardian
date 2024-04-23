namespace Enemy
{
    public class BossEnemy : EnemyController
    {
        // ---- / Public Variables / ---- //
        public delegate void WinGameEventHandler();
        public static event WinGameEventHandler OnWinGame;
        
        // ---- / Parent Variables / ---- //
        protected override float MoveSpeed => 1.5f;
        protected override int Health => 3;
        
        protected override void KillSelf()
        {
            OnWinGame?.Invoke();
            Destroy(gameObject);
        }
    }
}