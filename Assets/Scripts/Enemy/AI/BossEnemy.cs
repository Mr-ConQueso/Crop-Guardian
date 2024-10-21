namespace Enemy
{
    public class BossEnemy : EnemyController
    {
        // ---- / Events / ---- //
        public delegate void DefeatBossEventHandler();
        public static event DefeatBossEventHandler OnDefeatBoss;
        
        public override void KillSelf()
        {
            OnDefeatBoss?.Invoke();
            Destroy(gameObject);
        }
    }
}