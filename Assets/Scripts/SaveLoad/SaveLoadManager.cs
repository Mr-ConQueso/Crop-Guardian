using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SaveLoad
{
    public class SaveLoadManager : MonoBehaviour
    {
        private static string SavePath => $"{Application.persistentDataPath}/save.cg";
        
        public static void Save()
        {
            var state = LoadFile();
            CaptureState(state);
            SaveFile(state);
        }
        
        public static void Load()
        {
            var state = LoadFile();
            RestoreState(state);
        }
        
        [ContextMenu("Delete All Settings")]
        public void ResetAllSettings()
        {
            if (File.Exists(SavePath))
            {
                File.Delete(SavePath);
                Debug.Log("Settings have been reset. File deleted.");
            }
            else
            {
                Debug.LogWarning("No settings file found to reset.");
            }
        }

        private static Dictionary<string, object> LoadFile()
        {
            if (!File.Exists(SavePath))
            {
                return new Dictionary<string, object>();
            }

            using (FileStream stream = File.Open(SavePath, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }
        
        private static void SaveFile(object state)
        {
            using (var stream = File.Open(SavePath, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        private static void CaptureState(Dictionary<string, object> state)
        {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.Id] = saveable.CaptureState();
            }
        }

        private static void RestoreState(Dictionary<string, object> state)
        {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            {
                if (state.TryGetValue(saveable.Id, out object value))
                {
                    saveable.RestoreState(value);
                }
            }
        }
    }
}