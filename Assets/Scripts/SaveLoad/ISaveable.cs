namespace SaveLoad
{
    public interface ISaveable
    {
        object CaptureState();
        public void RestoreState(object state);
    }
}