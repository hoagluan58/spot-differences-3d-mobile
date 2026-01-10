using NFramework;
using UnityEngine;
using UnityEngine.UI;

namespace SpotDifferences
{
    public class HomeMenuUI : UIView
    {
        [SerializeField] private Button _startBTN;

        private void Awake()
        {
            _startBTN.onClick.AddListener(OnStartButtonClick);
        }

        private async void OnStartButtonClick()
        {
            CloseSelf();
            await SceneLoader.Load(Define.SceneName.GAME, true, true);
        }
    }
}
