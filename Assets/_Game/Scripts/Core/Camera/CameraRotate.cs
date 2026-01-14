using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public Transform target;
    public float rotateSpeed = 0.2f;
    public float minPitch = 15f;
    public float maxPitch = 75f;

    float yaw;
    float pitch;
    float distance;

    void Start()
    {
        var targetMidpoint = GetMidPoint(target);

        Vector3 offset = transform.position - targetMidpoint;
        distance = offset.magnitude;

        yaw = Mathf.Atan2(offset.x, offset.z) * Mathf.Rad2Deg;
        float horizontalDist = new Vector2(offset.x, offset.z).magnitude;
        pitch = Mathf.Atan2(offset.y, horizontalDist) * Mathf.Rad2Deg;
    }

    void Update()
    {
        Vector2 drag = Vector2.zero;

#if UNITY_EDITOR || UNITY_STANDALONE
        drag = HandleMouse();
#else
        drag = HandleTouch();
#endif


        // Prevent flipping over the top
        Vector3 camDir = (transform.position - target.position).normalized;
        if (Vector3.Dot(camDir, Vector3.up) > 0.95f && drag.y < 0) return;
        
        if (drag.sqrMagnitude < 0.0001f)
            return;

        yaw += drag.x * rotateSpeed * 100f;
        pitch -= drag.y * rotateSpeed * 100f;
        //pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        //Quaternion rot = Quaternion.Euler(pitch, yaw, 0);
        //Vector3 offset = rot * Vector3.back * distance;

        //transform.position = target.position + offset;
        //transform.rotation = rot;
        float radYaw = Mathf.Deg2Rad * yaw;
        float radPitch = Mathf.Deg2Rad * pitch;

        var targetMidpoint = GetMidPoint(target);
        Vector3 cameraPos;
        cameraPos.x = targetMidpoint.x + distance * Mathf.Cos(radPitch) * Mathf.Sin(radYaw);
        cameraPos.y = targetMidpoint.y + distance * Mathf.Sin(radPitch);
        cameraPos.z = targetMidpoint.z + distance * Mathf.Cos(radPitch) * Mathf.Cos(radYaw);

        transform.position = cameraPos;
        transform.LookAt(targetMidpoint);
    }

    private Vector2 HandleMouse()
    {
        var drag = Vector2.zero;
        if (Input.GetMouseButton(0))
        {
            drag = new Vector2(
                Input.GetAxis("Mouse X"),
                Input.GetAxis("Mouse Y")
            );
        }
        return drag;
    }

    private Vector2 HandleTouch()
    {
        var drag = Vector2.zero;
        if (Input.touchCount == 1 &&
              Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Touch t = Input.GetTouch(0);
            drag = t.deltaPosition * 0.01f; // 🔑 normalize pixels

        }
        return drag;
    }


    public Vector3 GetMidPoint(Transform target)
    {
        List<Collider> colliders = target.GetComponentsInChildren<Collider>().ToList();

        if (colliders == null || colliders.Count == 0)
            return Vector3.zero;

        Vector3 sum = Vector3.zero;
        int count = 0;

        foreach (var col in colliders)
        {
            if (col != null)
            {
                // Use the collider's bounds center
                sum += col.bounds.center;
                count++;
            }
        }

        if (count == 0) return Vector3.zero;

        return sum / count; // Average position = midpoint
    }

    void OnDrawGizmos()
    {
        Vector3 midpoint = GetMidPoint(target);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(midpoint, 1f);
    }
}
