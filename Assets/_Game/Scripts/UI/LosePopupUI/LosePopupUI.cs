using NFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SpotDifferences
{
    public class LosePopupUI : UIView
    {
        [SerializeField] private Button _homeBTN;
        [SerializeField] private Button _retryBTN;

        public Action OnHomeButtonClickedAction;
        public Action OnRetryButtonClickedAction;

        private void Awake()
        {
            _homeBTN.onClick.AddListener(OnHomeButtonClicked);
            _retryBTN.onClick.AddListener(OnRetryButtonClicked);
        }

        private void OnHomeButtonClicked()
        {
            OnHomeButtonClickedAction?.Invoke();
        }

        private void OnRetryButtonClicked()
        {
            OnRetryButtonClickedAction?.Invoke();
        }
    }
}
