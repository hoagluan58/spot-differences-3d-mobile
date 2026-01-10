using Cysharp.Threading.Tasks;
using NFramework;
using UnityEngine;
using UnityEngine.UI;

namespace SpotDifferences
{
    public class LoadingUI : UIView
    {
        [SerializeField] private Slider _fillLoadingIMG;

        private void SetFillAmount(float amount)
        {
            _fillLoadingIMG.normalizedValue = amount;
        }

        public async UniTask StartLoading(float loadingTime)
        {
            var timer = 0f;
            while (timer < loadingTime)
            {
                timer += Time.deltaTime;
                SetFillAmount(timer / loadingTime);
                await UniTask.Yield();
            }
            CloseSelf();
        }
    }
}
