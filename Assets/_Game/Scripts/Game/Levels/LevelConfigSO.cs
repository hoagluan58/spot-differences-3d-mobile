using NFramework;
using System.Collections.Generic;
using UnityEngine;

namespace SpotDifferences
{
    public class LevelConfigSO : GoogleSheetConfigSO<LevelConfig>
    {
        private Dictionary<string, LevelConfig> _levelConfigDic = new();

        public Dictionary<string, LevelConfig> LevelConfigDic => _levelConfigDic;

        public LevelConfig GetLevelConfig(string levelId) => _levelConfigDic[levelId];

        public void Init()
        {
            _datas.ForEach(data =>
            {
                _levelConfigDic[data.Id] = data;
            });
        }

        protected override void OnSynced(List<LevelConfig> googleSheetData)
        {
            base.OnSynced(googleSheetData);

            foreach (var item in googleSheetData)
            {
                item.LevelPrefab = FileHelper.LoadFirstAssetWithName<GameObject>(item.LevelPrefabName);
            }
        }
    }

    [System.Serializable]
    public class LevelConfig
    {
        public string Id;
        public GameObject LevelPrefab;
        public ELevelType Difficulty;
        public string Title;

        [HideInInspector]
        public string LevelPrefabName;
    }

    public enum ELevelType
    {
        None,
        Normal,
        Hard,
    }
}
