using NFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpotDifferences
{
    public class GamePopupUI : UIView
    {
        [SerializeField] private TextMeshProUGUI _levelTMP;
        [SerializeField] private Button _hintBTN;
        [SerializeField] private Button _defaultCamBTN;
        [SerializeField] private Button _settingsBTN;
        [SerializeField] private TextMeshProUGUI _hintLeftTMP;
        [SerializeField] private TextMeshProUGUI _timeLeftTMP;

        private void Awake()
        {
            _defaultCamBTN.onClick.AddListener(OnDefaultCamButtonClicked);
            _settingsBTN.onClick.AddListener(OnSettingsButtonClicked);
            _hintBTN.onClick.AddListener(OnHintButtonClicked);
        }

        private void OnEnable()
        {
            GameManager.OnSessionDataChanged += GameManager_OnSessionDataChanged;
        }

        private void OnDisable()
        {
            GameManager.OnSessionDataChanged -= GameManager_OnSessionDataChanged;
        }

        public void Init(string level, string title, SessionData sessionData)
        {
            SetLevelText($"Level {level} - {title}");
            SetTimeLeftText(sessionData.TimeLeft.ToString());
            SetHintLeftText(sessionData.HintCount.ToString());
        }

        private void SetLevelText(string level) => _levelTMP.SetText(level);

        private void SetTimeLeftText(string timeLeft) => _timeLeftTMP.SetText(timeLeft);

        private void SetHintLeftText(string hintLeft) => _hintLeftTMP.SetText(hintLeft);

        private void GameManager_OnSessionDataChanged(SessionData sessionData)
        {
            SetTimeLeftText(sessionData.TimeLeft.ToString());
            SetHintLeftText(sessionData.HintCount.ToString());
        }

        private void OnDefaultCamButtonClicked()
        {
            GameManager.I.ResetCam();
        }

        private void OnSettingsButtonClicked()
        {
            UIManager.OpenResources<SettingsPopupUI>(UIDefine.SETTINGS_POPUP_UI).Init(false);
        }

        private void OnHintButtonClicked()
        {
            GameManager.I.UseHint();
        }
    }
}
