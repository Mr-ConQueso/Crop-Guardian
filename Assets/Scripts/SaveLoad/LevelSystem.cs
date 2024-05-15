using System;
using UnityEngine;

namespace SaveLoad
{
    public class LevelSystem : MonoBehaviour, ISaveable
    {
        [SerializeField] private int level = 1;
        [SerializeField] private int xp = 20;
        [SerializeField] private Vector3 position = new Vector3(0, 0, 0);
        
        public object CaptureState()
        {
            return new SaveData()
            {
                level = level,
                xp = xp,
                position = position
            };
        }

        public void RestoreState(object state)
        {
            var saveData = (SaveData)state;

            level = saveData.level;
            xp = saveData.xp;
            position = saveData.position;
        }
        
        [Serializable]
        private struct SaveData
        {
            public int level;
            public int xp;
            public Vector3 position;
        }
    }
}