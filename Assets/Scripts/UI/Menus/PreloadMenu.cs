using SaveLoad;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreloadMenu : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [SerializeField] private TMP_Text loadingText;
    [SerializeField] private GameObject fadeInOut;

    // ---- / Private Variables / ---- //
    private int _dotsAmount;
    private const string Text = "Press Any\nButton To Start";

    private void Start()
    {
        InvokeRepeating(nameof(CycleDots), 0.7f, 0.7f);
        SaveLoadManager.Load();
        fadeInOut.SetActive(true);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("StartMenu");
        }
    }

    private void CycleDots()
    {
        if (_dotsAmount < 3)
        {
            _dotsAmount++;
        }
        else if (_dotsAmount >= 3)
        {
            _dotsAmount = 0;
        }
        
        loadingText.text = Text + ConvertDotsAmountToString(_dotsAmount);
    }

    private string ConvertDotsAmountToString(int dotsAmount)
    {
        return dotsAmount switch
        {
            0 => "",
            1 => ".",
            2 => "..",
            3 => "...",
            _ => ""
        };
    }
}