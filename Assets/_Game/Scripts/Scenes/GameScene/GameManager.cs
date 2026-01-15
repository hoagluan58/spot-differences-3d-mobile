using NFramework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace SpotDifferences
{
    public class GameManager : SingletonMono<GameManager>
    {
        private const int DEFAULT_SCREEN_COUNT = 2;
        private const int HARD_SCREEN_COUNT = 3;

        [SerializeField] private GameObject[] _screens;
        [SerializeField] private SerializedDictionary<int, LayerMask> _layerMapping;
        [SerializeField] private Transform _rootLevelTf;
        [SerializeField] private CameraRotate _cameraRotate;

        private Dictionary<int, GameLevel> _levelMapping = new Dictionary<int, GameLevel>();
        private LevelConfig _curLevelCfg;

        public void StartGame(string level)
        {
            _curLevelCfg = ConfigManager.I.LevelConfigDic[level];

            if (_curLevelCfg == null)
                return;

            _levelMapping.Clear();
            UpdateScreen();
            SpawnLevelPrefabs();
            _cameraRotate.Init(_levelMapping.Values.First().transform);
        }

        private void UpdateScreen()
        {
            for (var i = 0; i < DEFAULT_SCREEN_COUNT; i++)
            {
                _screens[i].SetActive(true);
            }

            _screens[2].SetActive(_curLevelCfg.Difficulty == ELevelType.Hard);
        }

        private void SpawnLevelPrefabs()
        {
            var count = _curLevelCfg.Difficulty == ELevelType.Hard ? HARD_SCREEN_COUNT : DEFAULT_SCREEN_COUNT;

            for (var i = 0; i < count; i++)
            {
                var level = Instantiate(_curLevelCfg.LevelPrefab, _rootLevelTf.transform).GetComponent<GameLevel>();
                level.Init(i);
                _levelMapping.Add(i, level);
            }

        }
    }
}
