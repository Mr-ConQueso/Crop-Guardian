using UnityEngine;

public class SavedSettings : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetFloat("HighestSurviveTimeKey", 0);
    }
}