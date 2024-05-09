using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnClick_StartGame()
    {
        SceneManager.LoadScene("LevelWaves");
    }

    public void OnClick_Exit()
    {
        MenuManager.OpenMenu(Menu.ExitMenu, gameObject);
    }

    public void OnClick_Settings()
    {
        MenuManager.OpenMenu(Menu.SettingsMenu, gameObject);
    }
    
    public void OnClick_Credits()
    {
        SceneManager.LoadScene("CreditsMenu");
    }
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnClick_Exit();
        }
    }
}
