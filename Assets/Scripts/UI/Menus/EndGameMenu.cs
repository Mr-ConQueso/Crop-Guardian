using BaseGame;
using TMPro;
using UnityEngine;

public class EndGameMenu : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;

    public void OnClick_BackToMainMenu()
    {
        SceneSwapManager.SwapScene("StartMenu");
    }
    
    private void Awake()
    {
        GameController.OnGameEnd += OnGameEnd;
    }

    private void OnDestroy()
    {
        GameController.OnGameEnd -= OnGameEnd;
    }
    
    private void OnGameEnd()
    {
        scoreText.text = GameController.Instance.CurrentScore.ToString();
        timeText.text = HelperFunctions.FormatTimer(GameController.Instance.TimerValue);
    }
}
