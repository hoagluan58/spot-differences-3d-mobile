using NFramework;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace SpotDifferences
{
    public class GameManager : SingletonMono<GameManager>
    {
        public static event Action<SessionData> OnSessionDataChanged;

        [SerializeField] private SerializedDictionary<ELevelType, GameObject> _screensDic;
        [SerializeField] private Transform _rootLevelTf;
        [SerializeField] private CameraRotate _cameraRotate;
        [SerializeField] private CameraZoom _cameraZoom;

        [SerializeField, ReadOnly] private SessionData _sessionData;

        private GameLevel _curGameLevel;
        private LevelConfig _curLevelCfg;
        private Coroutine _coroutine;

        public void StartGame(string level)
        {
            _curLevelCfg = ConfigManager.I.LevelConfigDic[level];
            _sessionData = new SessionData();

            UIManager.OpenResources<GamePopupUI>(UIDefine.GAME_POPUP_UI).Init((int.Parse(_curLevelCfg.Id) + 1).ToString(), _curLevelCfg.Title, _sessionData);

            if (_curLevelCfg == null)
                return;

            foreach (var kvp in _screensDic)
            {
                kvp.Value.SetActive(_curLevelCfg.Difficulty == kvp.Key);
            }

            _curGameLevel = Instantiate(_curLevelCfg.LevelPrefab, _rootLevelTf.transform).GetComponent<GameLevel>();
            _curGameLevel.Init();
            _cameraRotate.Init(_curGameLevel.transform);

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            // Check tutorial
            if (UserSaveData.I.IsNewPlayer)
            {
                UIManager.OpenResources<TutorialPopupUI>(UIDefine.TUTORIAL_POPUP_UI).ShowIntroducePopup(() =>
                {
                    StartCountdown();
                });
            }
            else
            {
                StartCountdown();
            }
        }

        public void HandleFoundItem(string id, int sceneFounded)
        {
            _curGameLevel.DestroyItem(id);
        }

        public void Win()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

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

        public void Lose()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            var losePopup = UIManager.OpenResources<LosePopupUI>(UIDefine.LOSE_POPUP_UI);

            losePopup.OnHomeButtonClickedAction = async () =>
            {
                await SceneLoader.Unload(Define.SceneName.GAME, Define.SceneName.MAIN);
                losePopup.CloseSelf();
                UIManager.Close(UIDefine.GAME_POPUP_UI);
                UIManager.OpenResources(UIDefine.HOME_MENU_UI);
            };

            losePopup.OnRetryButtonClickedAction = async () =>
            {
                Retry();
                losePopup.CloseSelf();
            };
        }

        public void ResetCam()
        {
            _cameraRotate.ResetCam();
            _cameraZoom.ResetCam();
        }

        public async void Retry()
        {
            var curLevelId = _curLevelCfg.Id;

            await SceneLoader.Unload(Define.SceneName.GAME, Define.SceneName.MAIN);
            UIManager.Close(UIDefine.GAME_POPUP_UI);
            await SceneLoader.Load(Define.SceneName.GAME, true, true);
            GameManager.I.StartGame(curLevelId);
        }

        public async void BackToHomeMenu()
        {
            await SceneLoader.Unload(Define.SceneName.GAME, Define.SceneName.MAIN);
            UIManager.Close(UIDefine.GAME_POPUP_UI);
            UIManager.OpenResources(UIDefine.HOME_MENU_UI);
        }

        public void UseHint()
        {
            if (!UserSaveData.I.IsShowHintTutorial)
            {
                UIManager.OpenResources<TutorialPopupUI>(UIDefine.TUTORIAL_POPUP_UI).ShowHintPopup(() =>
                {
                    _sessionData.HintCount--;
                    _curGameLevel.Hint();
                    OnSessionDataChanged?.Invoke(_sessionData);
                });
                return;
            }

            if (_sessionData.HintCount > 0)
            {
                _sessionData.HintCount--;
                _curGameLevel.Hint();
                OnSessionDataChanged?.Invoke(_sessionData);
            }
        }

        private void StartCountdown()
        {
            _coroutine = StartCoroutine(CRCountdown());

            IEnumerator CRCountdown()
            {
                var waitForSeconds = new WaitForSeconds(1f);

                while (_sessionData.TimeLeft > 0)
                {
                    _sessionData.TimeLeft -= 1f;
                    yield return waitForSeconds;
                    OnSessionDataChanged?.Invoke(_sessionData);
                }

                if (_sessionData.TimeLeft <= 0)
                {
                    Lose();
                }
            }
        }
    }

    [System.Serializable]
    public class SessionData
    {
        public int HintCount = 1;
        public float TimeLeft = 60;
    }
}
