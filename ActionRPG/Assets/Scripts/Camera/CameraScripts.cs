using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScripts : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0, 1, -10);

    public float zoomSpeed = 2f;
    public float minZoom = -5f;
    public float maxZoom = -20f;

    private float currentZoom;

    void Start()
    {
        currentZoom = offset.z;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            currentZoom += scroll * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, maxZoom, minZoom);

            Vector3 desiredOffset = new Vector3(offset.x, offset.y, currentZoom);
            Vector3 targetPosition = player.position + desiredOffset;

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }

    
}