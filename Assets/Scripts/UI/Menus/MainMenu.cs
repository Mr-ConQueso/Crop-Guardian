using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void OnClick_StartGame()
    {
        SceneManager.LoadScene("Level0", LoadSceneMode.Single);
    }

    public void OnClick_Exit()
    {
        MenuManager.OpenMenu(Menu.ExitMenu, gameObject);
    }

    public void OnClick_Settings()
    {
        MenuManager.OpenMenu(Menu.SettingsMenu, gameObject);
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
