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
        [SerializeField] private GameObject _winVFX;

        public Action OnHomeButtonClickedAction;
        public Action OnNextLevelButtonClickedAction;

        private void Awake()
        {
            _homeBTN.onClick.AddListener(OnHomeButtonClicked);
            _nextLevelBTN.onClick.AddListener(OnNextLevelButtonClicked);
        }

        private void OnEnable()
        {
            _winVFX.SetActive(true);
        }

        private void OnDisable()
        {
            _winVFX.SetActive(false);
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
