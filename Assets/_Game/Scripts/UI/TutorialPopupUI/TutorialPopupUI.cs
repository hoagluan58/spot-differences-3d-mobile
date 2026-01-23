using NFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SpotDifferences
{
    public class TutorialPopupUI : UIView
    {
        [SerializeField] private Button _useButton;
        [SerializeField] private Button _okButton;
        [SerializeField] private GameObject _hintPopupGo;
        [SerializeField] private GameObject _tutorialPopupGo;

        private Action _onClosePopupCb;

        private void Awake()
        {
            _useButton.onClick.AddListener(OnUseButtonClicked);
            _okButton.onClick.AddListener(OnOkButtonClicked);
        }

        private void OnUseButtonClicked()
        {
            CloseSelf();
            GameManager.I.UseHint();
            UserSaveData.I.IsNewPlayer = false;
        }

        private void OnOkButtonClicked()
        {
            _tutorialPopupGo.SetActive(false);
            _hintPopupGo.SetActive(true);
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
