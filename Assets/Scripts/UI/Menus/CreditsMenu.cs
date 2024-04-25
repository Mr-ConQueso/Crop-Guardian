using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour
{
    public void OnClick_GoBack()
    {   
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnClick_GoBack();
        }
    }
}
