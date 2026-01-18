using NFramework;
using UnityEngine;
using UnityEngine.UI;

namespace SpotDifferences
{
    public class SettingsPopupUI : UIView
    {
        [SerializeField] private Button _closeBTN;
        [SerializeField] private Button _overlayBTN;
        [SerializeField] private Button _homeBTN;
        [SerializeField] private Button _retryBTN;

        private void Awake()
        {
            _closeBTN.onClick.AddListener(OnCloseButtonClick);
            _overlayBTN.onClick.AddListener(OnOverlayButtonClick);
            _homeBTN.onClick.AddListener(OnHomeButtonClick);
            _retryBTN.onClick.AddListener(OnRetryButtonClick);
        }

        public void Init(bool isFromHomeMenu = false)
        {
            _retryBTN.gameObject.SetActive(!isFromHomeMenu);
            _homeBTN.gameObject.SetActive(!isFromHomeMenu);
        }

        private void OnEnable()
        {
            Time.timeScale = 0f;
        }

        private void OnDisable()
        {
            Time.timeScale = 1f;
        }

        private void OnCloseButtonClick()
        {
            CloseSelf();
        }

        private void OnOverlayButtonClick()
        {
            CloseSelf();
        }

        private void OnHomeButtonClick()
        {
            GameManager.I.BackToHomeMenu();
            CloseSelf();
        }

        private void OnRetryButtonClick()
        {
            GameManager.I.Retry();
            CloseSelf();
        }
    }
}
