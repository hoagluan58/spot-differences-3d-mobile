using Cysharp.Threading.Tasks;
using NFramework;
using UnityEngine;

namespace SpotDifferences
{
    public class BootManager : MonoBehaviour
    {
        private void Start()
        {
            Initialize().Forget();
        }

        private async UniTaskVoid Initialize()
        {
            await UniTask.Yield();

            Application.targetFrameRate = 60;
            Input.multiTouchEnabled = false;

            RegisterSaveData();

            await SoundManager.Initialize();
            await SoundManager.CacheSoundGroupResources(SoundDefine.SoundGroupKey.SOUND);
            await SceneLoader.Load(Define.SceneName.MAIN, true, true);
        }

        private void RegisterSaveData()
        {
            LocalSaveManager.RegisterSaveData(UserSaveData.I);
            LocalSaveManager.RegisterSaveData(SoundManager.I);
            LocalSaveManager.RegisterSaveData(VibrationManager.I);
            LocalSaveManager.Load();
        }
    }
}
