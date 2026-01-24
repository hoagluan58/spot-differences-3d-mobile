using NFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SpotDifferences
{
    public class TutorialPopupUI : UIView
    {
        [Header("HINT")]
        [SerializeField] private Button _useButton;
        [SerializeField] private GameObject _hintPopupGO;

        [Header("INTRODUCE")]
        [SerializeField] private GameObject _introducePopupGO;
        [SerializeField] private GameObject _controlPopupGO;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _okButton;

        private Action _onClosePopupCb;

        public void ShowIntroducePopup(Action onClosePopupCb = null)
        {
            _onClosePopupCb = onClosePopupCb;
            _introducePopupGO.SetActive(true);
            _controlPopupGO.SetActive(false);

            _nextButton.onClick.RemoveAllListeners();
            _okButton.onClick.RemoveAllListeners();

            _nextButton.onClick.AddListener(() =>
            {
                _introducePopupGO.gameObject.SetActive(false);
                _controlPopupGO.SetActive(true);
            });

            _okButton.onClick.AddListener(() =>
            {
                _controlPopupGO.SetActive(false);
                UserSaveData.I.IsNewPlayer = false;
                CloseSelf();
            });
        }

        public void ShowHintPopup(Action onClosePopupCb = null)
        {
            _onClosePopupCb = onClosePopupCb;
            _hintPopupGO.SetActive(true);

            _useButton.onClick.RemoveAllListeners();
            _useButton.onClick.AddListener(() =>
            {
                _hintPopupGO.SetActive(false);
                UserSaveData.I.IsShowHintTutorial = true;
                CloseSelf();
            });
        }

        public override UIOutputData OnClose()
        {
            _onClosePopupCb?.Invoke();
            return base.OnClose();
        }
    }
}
