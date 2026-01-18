using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpotDifferences
{
    public class ItemTrackerUI : MonoBehaviour
    {
        [SerializeField] private List<ItemTracker> _itemTrackers;

        private void OnEnable()
        {
            GameLevel.OnTrackItemInit += OnTrackItemInit;
            GameLevel.OnTrackItemUpdate += OnTrackItemUpdate;
        }

        private void OnDisable()
        {
            GameLevel.OnTrackItemInit -= OnTrackItemInit;
            GameLevel.OnTrackItemUpdate -= OnTrackItemUpdate;
        }

        private void OnTrackItemInit(int count)
        {
            _itemTrackers.ForEach(x => x.SetActive(false));

            for (int i = 0; i < count; i++)
            {
                _itemTrackers[i].SetActive(true);
                _itemTrackers[i].SetTrack(false);
            }
        }

        private void OnTrackItemUpdate(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _itemTrackers[i].SetTrack(true);
            }
        }
    }
}
