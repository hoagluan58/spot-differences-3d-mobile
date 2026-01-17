using System.Collections.Generic;
using UnityEngine;

namespace SpotDifferences
{
    public class GameLevel : MonoBehaviour
    {
        private Dictionary<int, List<GameItem>> _gameItems = new Dictionary<int, List<GameItem>>();

        public void Init()
        {
        }
    }
}
