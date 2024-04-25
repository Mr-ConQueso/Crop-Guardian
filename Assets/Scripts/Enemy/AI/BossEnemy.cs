namespace Enemy
{
    public class BossEnemy : EnemyController
    {
        // ---- / Parent Variables / ---- //
        protected override float MoveSpeed => 1.5f;
        protected override int Health => 3;
        
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