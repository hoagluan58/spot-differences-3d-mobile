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

        private void Awake()
        {
            _defaultCamBTN.onClick.AddListener(OnDefaultCamButtonClicked);
        }

        public void Init(string level)
        {
            _levelTMP.text = $"Level {level}";
        }

        private void OnDefaultCamButtonClicked()
        {
            GameManager.I.ResetCam();
        }
    }
}
