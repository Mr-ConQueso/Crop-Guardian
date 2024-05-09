namespace Enemy
{
    public class BossEnemy : EnemyController
    {
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