using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpotDifferences
{
    public class GameLevel : MonoBehaviour
    {
        public static event Action<int> OnTrackItemInit;
        public static event Action<int> OnTrackItemUpdate;

        private Dictionary<string, List<GameItem>> _gameItems = new Dictionary<string, List<GameItem>>();
        private int _currentItemIndex = 0;

        public void Init()
        {
            var gameItems = GetComponentsInChildren<GameItem>();

            foreach (var gameItem in gameItems)
            {
                if (!_gameItems.ContainsKey(gameItem.Id))
                {
                    _gameItems.Add(gameItem.Id, new List<GameItem> { gameItem });
                }
                else
                {
                    _gameItems[gameItem.Id].Add(gameItem);
                }
            }

            _currentItemIndex = 0;
            OnTrackItemInit?.Invoke(_gameItems.Keys.Count);
        }

        public void DestroyItem(string id)
        {
            _currentItemIndex++;
            _gameItems[id].ForEach(item => item.SetActive(false));
            OnTrackItemUpdate?.Invoke(_currentItemIndex);
            if (_currentItemIndex == _gameItems.Keys.Count)
            {
                GameManager.I.Win();
            }
        }
    }
}
