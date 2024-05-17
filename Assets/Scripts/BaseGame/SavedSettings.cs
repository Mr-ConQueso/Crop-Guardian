using System;
using SaveLoad;
using UnityEngine;

namespace BaseGame
{
    public class SavedSettings : MonoBehaviour, ISaveable
    {
        // ---- / Singleton / ---- //
        public static SavedSettings Instance;
        
        // ---- / Public Variables / ---- //
        // ---- /  / ---- //
        public static float mouseVerticalSensibility = 2.5f;
        public static float mouseHorizontalSensibility = 5.0f;
        public static bool invertDirection;
        
        // ---- /  / ---- //
        public static int lastScore;
        public static int highestScore;
        public static float lastTime;
        public static float highestTime;
        
        // ---- /  / ---- //
        public static int graphicsQuality;
        public static int fullScreenMode;

        public static int audioMode;
        public static float masterVolume;
        public static float musicVolume;
        public static float sfxVolume;
        

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            SaveLoadManager.Load();
        }
        
        public object CaptureState()
        {
            return new SaveData()
            {
                currentQualityIndex = graphicsQuality,
                currentAudioModeIndex = audioMode,
                currentFullScreenModeIndex = fullScreenMode,
            
                masterVolume = masterVolume,
                musicVolume = musicVolume,
                sfxVolume = sfxVolume
            };
        }

        public void RestoreState(object state)
        {
            var saveData = (SaveData)state;

            graphicsQuality = saveData.currentQualityIndex;
            audioMode = saveData.currentAudioModeIndex;
            fullScreenMode = saveData.currentFullScreenModeIndex;
        
            masterVolume = saveData.masterVolume;
            musicVolume = saveData.musicVolume;
            sfxVolume = saveData.sfxVolume;
        }
    
        [Serializable]
        private struct SaveData
        {
            public int currentAudioModeIndex;
            public int currentQualityIndex;
            public int currentFullScreenModeIndex;

            public float masterVolume;
            public float musicVolume;
            public float sfxVolume;
        }
    }
}