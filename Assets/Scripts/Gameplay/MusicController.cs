using UnityEngine;

public class MusicController : MonoBehaviour
{
    // ---- / Private Variables / ---- //
    private AudioSource _musicSource;
    private float _lastPlayedTime;
    
    public void SaveLastPlayedTime()
    {
        _lastPlayedTime = _musicSource.time;
    }
    
    public void PLayAtLastPlayedTime()
    {
        PlayMusicAtTime(_lastPlayedTime);
    }
    
    private void Start()
    {
        _musicSource = GetComponent<AudioSource>();
        PLayAtLastPlayedTime();
    }

    private void PlayMusicAtTime(float timeWanted)
    {
        if (timeWanted <= _musicSource.clip.length)
        {
            _musicSource.time = timeWanted;
            _musicSource.Play();
        }
    }
}
