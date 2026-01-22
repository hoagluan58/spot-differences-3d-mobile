using EPOOutline;
using Redcode.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace SpotDifferences
{
    public class GameItem : MonoBehaviour
    {
        [SerializeField] private string _id;

        public string Id => _id;

        private Outlinable _outlinable;
        private List<Renderer> _renderers = new List<Renderer>();
        private void Awake()
        {
            _outlinable = this.GetOrAddComponent<Outlinable>();
            this.GetComponents(_renderers);
            _renderers.ForEach((renderer) => _outlinable.AddRenderer(renderer));

            ToggleOutline(false);
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void ToggleOutline(bool isActive)
        {
            _outlinable.enabled = isActive;
        }
    }
}
