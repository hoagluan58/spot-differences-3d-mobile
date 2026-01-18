using NFramework;
using UnityEngine;
using UnityEngine.UI;

namespace SpotDifferences
{
    public class LosePopupUI : UIView
    {
        [SerializeField] private Button _homeBTN;
        [SerializeField] private Button _retryBTN;

        private void Awake()
        {
            _homeBTN.onClick.AddListener(OnHomeButtonClicked);
            _retryBTN.onClick.AddListener(OnRetryButtonClicked);
        }

        private void OnHomeButtonClicked()
        {
            CloseSelf();
        }

        private void OnRetryButtonClicked()
        {
            CloseSelf();
        }
    }
}
