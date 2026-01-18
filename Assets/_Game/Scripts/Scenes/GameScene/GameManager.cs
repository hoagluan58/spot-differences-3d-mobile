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
        [SerializeField] private CameraZoom _cameraZoom;

        private GameLevel _curGameLevel;
        private LevelConfig _curLevelCfg;

        public void StartGame(string level)
        {
            _curLevelCfg = ConfigManager.I.LevelConfigDic[level];
            UIManager.OpenResources<GamePopupUI>(UIDefine.GAME_POPUP_UI).Init((int.Parse(_curLevelCfg.Id) + 1).ToString());

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
            var nextLevelId = int.Parse(_curLevelCfg.Id) + 1;
            var nextLevelConfig = ConfigManager.I.LevelConfigDic[nextLevelId.ToString()];
            var winPopup = UIManager.OpenResources<WinPopupUI>(UIDefine.WIN_POPUP_UI);

            if (nextLevelConfig != null)
            {
                UserSaveData.I.UnlockLevel(nextLevelConfig.Id);
            }
            winPopup.OnHomeButtonClickedAction = async () =>
            {
                await SceneLoader.Unload(Define.SceneName.GAME, Define.SceneName.MAIN);
                winPopup.CloseSelf();
                UIManager.Close(UIDefine.GAME_POPUP_UI);
                UIManager.OpenResources(UIDefine.HOME_MENU_UI);
            };

            winPopup.OnNextLevelButtonClickedAction = async () =>
            {
                await SceneLoader.Unload(Define.SceneName.GAME, Define.SceneName.MAIN);
                winPopup.CloseSelf();
                UIManager.Close(UIDefine.GAME_POPUP_UI);

                if (nextLevelConfig != null)
                {
                    await SceneLoader.Load(Define.SceneName.GAME, true, true);
                    GameManager.I.StartGame(nextLevelConfig.Id);
                }
                else
                {
                    UIManager.OpenResources(UIDefine.HOME_MENU_UI);
                }
            };
        }

        public void ResetCam()
        {
            _cameraRotate.ResetCam();
            _cameraZoom.ResetCam();
        }
    }
}
