using BaseGame;
using UnityEngine;

public class EndGameMenu : MonoBehaviour
{
    public void OnClick_BackToMainMenu()
    {
        SceneSwapManager.SwapScene("StartMenu");
    }
}
