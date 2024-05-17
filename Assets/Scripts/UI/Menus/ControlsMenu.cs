using System;
using BaseGame;
using SaveLoad;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menus
{
    public class ControlsMenu : MonoBehaviour, ISaveable
    {
        [Header("Sliders")]
        [SerializeField] private Slider turretSensibilitySlider;
        [SerializeField] private Slider mouseSensibilitySlider;
        
        [Header("Buttons")]
        [SerializeField] private TMP_Text invertMouseButtonText;
        
        private void OnEnable()
        {
            SaveLoadManager.Load();
        }
        
        public void OnClick_Back()
        {
            SaveLoadManager.Save();
            MenuManager.OpenMenu(Menu.SettingsMenu, gameObject);
        }

        public void OnClick_InvertMouse()
        {
            if (SavedSettings.invertDirection == -1)
            {
                SavedSettings.invertDirection = 1;
                invertMouseButtonText.text = "Inverted";
            }
            else
            {
                SavedSettings.invertDirection = -1;
                invertMouseButtonText.text = "Normal";
            }
        }
        
        public void SetMouseSensibility(float sliderValue)
        {
            SavedSettings.mouseVerticalSensibility = sliderValue;
        }
        
        public void SetTurretSensibility(float sliderValue)
        {
            SavedSettings.mouseHorizontalSensibility = sliderValue;
        }
        
        public object CaptureState()
        {
            return new SaveData()
            {
                invertMouseText = invertMouseButtonText.text
            };
        }

        public void RestoreState(object state)
        {
            var saveData = (SaveData)state;

            invertMouseButtonText.text = saveData.invertMouseText;
            mouseSensibilitySlider.value = SavedSettings.mouseVerticalSensibility;
            turretSensibilitySlider.value = SavedSettings.mouseHorizontalSensibility;
        }

        [Serializable]
        private struct SaveData
        {
            public string invertMouseText;
        }
    }
}