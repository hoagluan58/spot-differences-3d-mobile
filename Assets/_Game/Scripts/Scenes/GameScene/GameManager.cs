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
            UIManager.OpenResources<GamePopupUI>(UIDefine.GAME_POPUP_UI).Init(level);
            _curLevelCfg = ConfigManager.I.LevelConfigDic[level];

            if (_curLevelCfg == null)
                return;

            foreach (var kvp in _screensDic)
            {
                kvp.Value.SetActive(_curLevelCfg.Difficulty == kvp.Key);
            }

            _curGameLevel = Instantiate(_curLevelCfg.LevelPrefab, _rootLevelTf.transform).GetComponent<GameLevel>();
            _curGameLevel.Init();
            _cameraRotate.Init(_curGameLevel.transform);
        }

        public void HandleFoundItem(string id, int sceneFounded)
        {
            _curGameLevel.DestroyItem(id);
        }

        public void Win()
        {
            var winPopup = UIManager.OpenResources<WinPopupUI>(UIDefine.WIN_POPUP_UI);

            winPopup.OnHomeButtonClickedAction = async () =>
            {
                await SceneLoader.Unload(Define.SceneName.GAME, Define.SceneName.MAIN);
                UIManager.Close(UIDefine.GAME_POPUP_UI);
                winPopup.CloseSelf();
            };

            winPopup.OnNextLevelButtonClickedAction = async () =>
            {
                await SceneLoader.Unload(Define.SceneName.GAME, Define.SceneName.MAIN);
            };
        }
    }
}
