using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    public void OnClick_GoBack()
    {   
        MenuManager.OpenMenu(Menu.MainMenu, gameObject);
    }
    
    private void Update()
    {
        if (InputManager.WasEscapePressed)
        {
            OnClick_GoBack();
        }
    }
}
