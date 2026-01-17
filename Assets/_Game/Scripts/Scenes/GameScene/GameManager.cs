using NFramework;
using UnityEngine;
using UnityEngine.Rendering;

namespace SpotDifferences
{
    public class GameManager : SingletonMono<GameManager>
    {
        [SerializeField] private SerializedDictionary<ELevelType, GameObject> _screensDic;
        [SerializeField] private Transform _rootLevelTf;
        [SerializeField] private CameraRotate _cameraRotate;

        private GameLevel _curGameLevel;
        private LevelConfig _curLevelCfg;

        public void StartGame(string level)
        {
            _curLevelCfg = ConfigManager.I.LevelConfigDic[level];

            if (_curLevelCfg == null)
                return;

            foreach (var kvp in _screensDic)
            {
                kvp.Value.SetActive(_curLevelCfg.Difficulty == kvp.Key);
            }

            _curGameLevel = Instantiate(_curLevelCfg.LevelPrefab, _rootLevelTf.transform).GetComponent<GameLevel>();
            _cameraRotate.Init(_curGameLevel.transform);
        }

        public void HandleFoundItem()
        {
        }
    }
}
