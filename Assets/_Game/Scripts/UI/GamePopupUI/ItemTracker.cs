using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpotDifferences
{
    public class ItemTracker : MonoBehaviour
    {
        [SerializeField] private GameObject _activeGo;

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public void SetTrack(bool isTracked) => _activeGo.SetActive(isTracked);
    }
}
