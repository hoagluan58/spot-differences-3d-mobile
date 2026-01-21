using NFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SpotDifferences
{
    public class TutorialPopupUI : UIView
    {
        [SerializeField] private Button _useButton;

        private Action _onClosePopupCb;

        private void Awake()
        {
            _useButton.onClick.AddListener(OnUseButtonClicked);
        }

        private void OnUseButtonClicked()
        {
            CloseSelf();
            GameManager.I.UseHint();
            UserSaveData.I.IsNewPlayer = false;
        }

        public void Init(Action onClosePopupCb = null)
        {
            _onClosePopupCb = onClosePopupCb;
        }

        public override UIOutputData OnClose()
        {
            _onClosePopupCb?.Invoke();
            return base.OnClose();
        }
    }
}
