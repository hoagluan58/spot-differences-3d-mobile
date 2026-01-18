using NFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpotDifferences
{
    public class LevelItemUI : MonoBehaviour
    {
        [Header("Unlocked")]
        [SerializeField] private GameObject _unlockGo;
        [SerializeField] private TextMeshProUGUI _levelTMP;
        [SerializeField] private Button _unlockBTN;

        [Header("Locked")]
        [SerializeField] private GameObject _lockGo;
        [SerializeField] private TextMeshProUGUI _lockTMP;
        [SerializeField] private Button _lockBTN;

        [Header("General")]
        [SerializeField] private GameObject _connectLineGo;

        private string _levelId;
        public string LevelId => _levelId;

        private void Awake()
        {
            _unlockBTN.onClick.AddListener(OnUnlockButtonClicked);
            _lockBTN.onClick.AddListener(OnLockButtonClicked);
        }

        private async void OnUnlockButtonClicked()
        {
            await SceneLoader.Load(Define.SceneName.GAME, true, true);
            UIManager.Close(UIDefine.HOME_MENU_UI);
            GameManager.I.StartGame(_levelId);
        }

        private void OnLockButtonClicked()
        {

        }

        public void SetData(string levelId, bool isLocked)
        {
            _levelId = levelId;
            SetLock(isLocked);
            SetLevelText(levelId);
        }

        public void SetLock(bool isLock)
        {
            _unlockGo.SetActive(!isLock);
            _lockGo.SetActive(isLock);
        }

        public void SetConnectLine(bool isActive)
        {
            _connectLineGo.SetActive(isActive);
        }

        private void SetLevelText(string text)
        {
            _levelTMP.text = text;
            _lockTMP.text = text;
        }
    }
}
