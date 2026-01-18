using NFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SpotDifferences
{
    public class WinPopupUI : UIView
    {
        [SerializeField] private Button _homeBTN;
        [SerializeField] private Button _nextLevelBTN;

        public Action OnHomeButtonClickedAction;
        public Action OnNextLevelButtonClickedAction;

        private void Awake()
        {
            _homeBTN.onClick.AddListener(OnHomeButtonClicked);
            _nextLevelBTN.onClick.AddListener(OnNextLevelButtonClicked);
        }

        private void OnHomeButtonClicked()
        {
            OnHomeButtonClickedAction?.Invoke();
        }

        private void OnNextLevelButtonClicked()
        {
            OnNextLevelButtonClickedAction?.Invoke();
        }
    }
}
