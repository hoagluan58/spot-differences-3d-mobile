using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpotDifferences
{
    public class LevelScrollView : MonoBehaviour
    {
        [SerializeField] private LevelItemUI _levelItemTopPf;
        [SerializeField] private LevelItemUI _levelItemBottomPf;
        [SerializeField] private ScrollRect _scrollRect;

        private List<LevelItemUI> _levelItems = new List<LevelItemUI>();
        private bool _isInit;

        public void SetData()
        {
            var cfg = ConfigManager.I.LevelConfigDic;

            if (_isInit)
            {
                UpdateData();
                return;
            }

            var index = 0;

            foreach (var kvp in cfg)
            {
                var isLastIndex = index == cfg.Keys.Count - 1;
                var levelCfg = kvp.Value;

                if (index % 2 == 0)
                {
                    var levelItem = Instantiate(_levelItemBottomPf, _scrollRect.content);
                    levelItem.SetData(levelCfg.Id, !UserSaveData.I.IsLevelUnlocked(kvp.Key));
                    levelItem.SetConnectLine(!isLastIndex);
                    _levelItems.Add(levelItem);
                }
                else
                {
                    var levelItem = Instantiate(_levelItemTopPf, _scrollRect.content);
                    levelItem.SetData(levelCfg.Id, !UserSaveData.I.IsLevelUnlocked(kvp.Key));
                    levelItem.SetConnectLine(!isLastIndex);
                    _levelItems.Add(levelItem);
                }

                index++;
            }

            _isInit = true;
        }

        private void UpdateData()
        {
            _levelItems.ForEach(levelItem =>
            {
                var leveId = levelItem.LevelId;
                levelItem.SetLock(!UserSaveData.I.IsLevelUnlocked(leveId));
            });
        }
    }
}
