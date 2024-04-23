using UnityEngine;

public class PreloadMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            MenuManager.OpenMenu(Menu.MainMenu, gameObject);
        }
    }
}