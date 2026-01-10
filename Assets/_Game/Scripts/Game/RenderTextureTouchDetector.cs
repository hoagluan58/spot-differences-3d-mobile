using UnityEngine;

namespace SpotDifferences
{
    public class RenderTextureTouchDetector : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _maxHoldDuration;
        [SerializeField] private float _holdDuration;

        private Camera _mainCamera;
        private RaycastHit[] _hitBuffer = new RaycastHit[5];
        private GameObject _lastHitObject;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

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
                if (IsTouchObject())
                { 
                    HandleObject();
                }
                _holdDuration = 0f;
            }
        }

        private GameObject HandleTouch(Vector2 screenPos)
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

            var hitCount = Physics.RaycastNonAlloc(sceneRay, _hitBuffer, 1000f, _layerMask);

            if (hitCount == 0)
            {
                return null;
            }

            RaycastHit closestHit = _hitBuffer[0];

            for (var i = 1; i < hitCount; i++)
            {
                if (_hitBuffer[i].distance < closestHit.distance)
                    closestHit = _hitBuffer[i];
            }

            return closestHit.collider.gameObject;
        }

        private void HandleObject()
        {
            Debug.Log($"{_lastHitObject.name}");
            _lastHitObject = null;
        }

        private bool IsTouchObject()
        {
            return _holdDuration < _maxHoldDuration && _lastHitObject != null;
        }
    }
}
