using Cysharp.Threading.Tasks;
using NFramework;
using UnityEngine;

namespace SpotDifferences
{
    public class GameManager : SingletonMono<GameManager>
    {
        private void Start()
        {
            Initialize().Forget();
        }

        private async UniTaskVoid Initialize()
        {
            
        }
    }
}
