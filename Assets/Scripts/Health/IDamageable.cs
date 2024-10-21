namespace Health
{
    public interface IDamageable
    {
        public void RemoveHealth(float amount) { }
        
        public void AddHealth(float amount) { }
        
        public void KillSelf() { }
    }
}