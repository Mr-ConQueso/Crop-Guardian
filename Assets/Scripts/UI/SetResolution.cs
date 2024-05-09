using UnityEngine;

public class SetResolution : MonoBehaviour
{
    // ---- / Private Variables / ---- //
    private float _lastWidth;
    private float _lastHeight;
    
    private void Awake()
    {
        Screen.SetResolution(1440, 1080, Screen.fullScreenMode);
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if(Mathf.Approximately(_lastWidth, Screen.width))
        {
            Screen.SetResolution(Screen.width, (int)(Screen.width * (16f / 9f)), Screen.fullScreenMode);
        }
        else if(Mathf.Approximately(_lastHeight, Screen.height))
        {
            Screen.SetResolution((int)(Screen.height * (9f / 16f)), Screen.height, Screen.fullScreenMode);
        }

        _lastWidth = Screen.width;
        _lastHeight = Screen.height;
    }
}