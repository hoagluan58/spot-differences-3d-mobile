using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpotDifferences
{
    public class CameraZoom : MonoBehaviour
    {
        public List<Camera> cameras;

        public float zoomSpeed = 10f;
        public float minFOV = 2f;
        public float maxFOV = 20f;


        private void Start()
        {
            cameras = new List<Camera>(GetComponentsInChildren<Camera>());
        }

        private void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            HandleMouse();
#else
            HandleTouch();
#endif
        }

        private void HandleMouse()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll == 0) return;

            foreach (var cam in cameras)
            {
                cam.fieldOfView -= scroll * zoomSpeed;
                cam.fieldOfView = Mathf.Clamp(
                    cam.fieldOfView, minFOV, maxFOV
                );
            }
        }

        private void HandleTouch()
        {
            if (Input.touchCount == 2)
            {
                Touch a = Input.GetTouch(0);
                Touch b = Input.GetTouch(1);

                float prev = (a.position - a.deltaPosition -
                              (b.position - b.deltaPosition)).magnitude;
                float curr = (a.position - b.position).magnitude;

                float delta = curr - prev;
                foreach(var cam in cameras)
                {
                    cam.fieldOfView -= delta * zoomSpeed * 0.01f;
                    cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minFOV, maxFOV);
                }
            }
        }
    }
}
