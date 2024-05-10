using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    public void OnClick_BackToMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
