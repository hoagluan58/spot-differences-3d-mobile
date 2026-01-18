using NFramework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpotDifferences
{
    public class GamePopupUI : UIView
    {
        [SerializeField] private TextMeshProUGUI _levelTMP;
        [SerializeField] private Button _hintBTN;
        [SerializeField] private Button _defaultCamBTN;

        public void Init(string level)
        {
            _levelTMP.text = level;
        }

    }
}
