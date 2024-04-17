using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{
    // ---- / Serialized Variables / ---- //

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SettingsScreen()
    {
        
    }
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
    }
}
