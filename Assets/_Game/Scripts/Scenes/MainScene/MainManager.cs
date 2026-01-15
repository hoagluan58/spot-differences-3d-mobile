using NFramework;
using UnityEngine;

namespace SpotDifferences
{
    public class MainManager : MonoBehaviour
    {
        private async void Start()
        {
            var loadingUI = UIManager.OpenResources<LoadingUI>(UIDefine.LOADING_UI);

            loadingUI.SetStartButtonAction(() =>
            {
                UIManager.OpenResources(UIDefine.HOME_MENU_UI);
            });

            await loadingUI.StartLoading(1f);
        }
    }
}
