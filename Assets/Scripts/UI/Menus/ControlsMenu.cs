using BaseGame;
using UnityEngine;

namespace UI.Menus
{
    public class ControlsMenu : MonoBehaviour
    {
        public void OnClick_Back()
        {
            MenuManager.OpenMenu(Menu.SettingsMenu, gameObject);
        }

        public void OnClick_InvertMouse()
        {
            SavedSettings.invertDirection = !SavedSettings.invertDirection;
        }
        
        public void SetMouseSensibility(float sliderValue)
        {
            SavedSettings.mouseVerticalSensibility += sliderValue;
        }
    }
}