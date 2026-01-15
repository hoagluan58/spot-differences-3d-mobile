using NFramework;
using UnityEngine;
using UnityEngine.UI;

namespace SpotDifferences
{
    public class HomeMenuUI : UIView
    {
        [SerializeField] private Button _settingsBTN;
        [SerializeField] private LevelScrollView _levelScrollView;

        private void Awake()
        {
            _settingsBTN.onClick.AddListener(OnSettingsButtonClicked);
        }

        public override void OnOpen(UIInputData inputData)
        {
            base.OnOpen(inputData);
            Init();
        }
        
        private void Init()
        {
            _levelScrollView.SetData();
        }

        private void OnSettingsButtonClicked()
        {

        }
    }
}
