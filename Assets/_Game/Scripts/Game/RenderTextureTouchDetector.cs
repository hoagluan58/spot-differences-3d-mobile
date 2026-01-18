using UnityEngine;

namespace SpotDifferences
{
    public class RenderTextureTouchDetector : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _maxHoldDuration;
        [SerializeField] private int _sceneIndex;

        private float _holdDuration;
        private RaycastHit[] _hitBuffer = new RaycastHit[5];
        private GameItem _lastHitObject;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _lastHitObject = HandleTouch(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                _holdDuration += Time.deltaTime;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (IsValidTouch())
                {
                    if (_lastHitObject != null)
                    {
                        HandleObject();
                    }
                    else
                    {
                        Debug.Log("No object found");
                    }
                }
                _holdDuration = 0f;
            }
        }

        private GameItem HandleTouch(Vector2 screenPos)
        {
            Ray ray = _mainCamera.ScreenPointToRay(screenPos);

            if (!Physics.Raycast(ray, out RaycastHit planeHit))
                return null;

            if (planeHit.collider.gameObject != gameObject)
                return null;

            Vector2 uv = planeHit.textureCoord;

            Ray sceneRay = _camera.ViewportPointToRay(
                new Vector3(uv.x, uv.y, 0f)
            );

            var hitCount = Physics.RaycastNonAlloc(sceneRay, _hitBuffer, Mathf.Infinity, _layerMask);

            if (hitCount == 0)
            {
                return null;
            }

            GameItem closestItem = null;
            float closestDist = float.MaxValue;


            for (int i = 0; i < hitCount; i++)
            {
                RaycastHit hit = _hitBuffer[i];

                if (!hit.collider.TryGetComponent(out GameItem item))
                    continue;

                if (hit.distance < closestDist)
                {
                    closestDist = hit.distance;
                    closestItem = item;
                }
            }

            return closestItem;
        }

        private void HandleObject()
        {
            GameManager.I.HandleFoundItem(_lastHitObject.Id, _sceneIndex);
            _lastHitObject = null;
        }

        private bool IsValidTouch()
        {
            return _holdDuration < _maxHoldDuration;
        }
    }
}
