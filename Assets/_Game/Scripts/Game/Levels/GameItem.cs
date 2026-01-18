using UnityEngine;

namespace SpotDifferences
{
    public class GameItem : MonoBehaviour
    {
        [SerializeField] private string _id;
        [SerializeField] private int _sceneId;

        public string Id => _id;
        public int SceneId => _sceneId;

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
