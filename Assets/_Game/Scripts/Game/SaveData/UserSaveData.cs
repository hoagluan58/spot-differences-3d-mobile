using NFramework;
using System.Collections.Generic;
using UnityEngine;

namespace SpotDifferences
{
    public class UserSaveData : SingletonMono<UserSaveData>, ISaveable
    {
        [SerializeField] private SaveData _saveData;

        public bool IsNewPlayer 
        { 
            get => _saveData.IsNewPlayer;
            set {
                _saveData.IsNewPlayer = value;
                DataChanged = true;
            }
        }

        public void UnlockLevel(string level)
        {
            _saveData.LevelUnlocked.Add(level);
            DataChanged = true;
        }

        public bool IsLevelUnlocked(string level) => _saveData.LevelUnlocked.Contains(level);

        #region ISaveable

        [System.Serializable]
        public class SaveData
        {
            public List<string> LevelUnlocked = new List<string>() { "0" };
            public bool IsNewPlayer = true;
        }

        public string SaveKey => Define.SaveKey.USER_SAVE_DATA;

        public bool DataChanged { get; set; }

        public object GetData()
        {
            return _saveData;
        }

        public void OnAllDataLoaded()
        {
        }

        public void SetData(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                _saveData = new SaveData();
                DataChanged = true;
            }
            else
            {
                _saveData = JsonUtility.FromJson<SaveData>(data);
            }
        }
        #endregion
    }
}
