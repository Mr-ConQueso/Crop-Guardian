using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    public void OnClick_GoBack()
    {   
        MenuManager.OpenMenu(Menu.MainMenu, gameObject);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnClick_GoBack();
        }
    }
}
