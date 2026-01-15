using NFramework;
using System.Collections.Generic;
using UnityEngine;

namespace SpotDifferences
{
    public class ConfigManager : SingletonMono<ConfigManager>
    {
        [SerializeField] private LevelConfigSO _levelConfigSO;

        public Dictionary<string, LevelConfig> LevelConfigDic => _levelConfigSO.LevelConfigDic;

        protected override void Awake()
        {
            base.Awake();
            Init();
        }

        private void Init()
        {
            _levelConfigSO.Init();
        }
    }
}
