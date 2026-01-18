using UnityEngine;

namespace SpotDifferences
{
    public class GameItem : MonoBehaviour
    {
        [SerializeField] private string _id;

        public string Id => _id;

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
