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
        public static float mouseHorizontalSensibility = 60.0f;
        public static int invertDirection = -1;
        
        // ---- /  / ---- //
        public static int lastScore = 0;
        public static int highestScore = 0;
        public static float lastTime = 0.0f;
        public static float highestTime = 0.0f;
        
        // ---- /  / ---- //
        public static int graphicsQuality = 0;
        public static int fullScreenMode = 1;

        public static int audioMode = 0;
        public static float masterVolume = 1;
        public static float musicVolume = 1;
        public static float sfxVolume = 1;
        

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
                sfxVolume = sfxVolume,
                
                invertMouse = invertDirection,
                verticalSensibility = mouseVerticalSensibility,
                horizontalSensibility = mouseHorizontalSensibility,
                
                lastScore = lastScore,
                highScore = highestScore,
                lastTime = lastTime,
                highTime = highestTime
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

            invertDirection = saveData.invertMouse;
            mouseVerticalSensibility = saveData.verticalSensibility;
            mouseHorizontalSensibility = saveData.horizontalSensibility;

            lastScore = saveData.lastScore;
            highestScore = saveData.highScore;
            lastTime = saveData.lastTime;
            highestTime = saveData.highTime;
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

            public int invertMouse;
            public float verticalSensibility;
            public float horizontalSensibility;

            public int lastScore;
            public int highScore;
            public float lastTime;
            public float highTime;
        }
    }
}