using Cysharp.Threading.Tasks;
using NFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SpotDifferences
{
    public class LoadingUI : UIView
    {
        [SerializeField] private Slider _fillLoadingIMG;
        [SerializeField] private Button _startBTN;

        private Action _onStartButtonAction;

        private void Awake()
        {
            _startBTN.onClick.AddListener(() =>
            {
                _onStartButtonAction?.Invoke();
                CloseSelf();
            });
        }

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

            _fillLoadingIMG.gameObject.SetActive(false);
            _onStartButtonAction?.Invoke();
            CloseSelf();
        }

        public void SetStartButtonAction(Action action)
        {
            _onStartButtonAction = action;
        }
    }
}
