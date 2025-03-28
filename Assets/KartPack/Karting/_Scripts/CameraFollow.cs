using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0f;
    public Vector3 offset;

    private void LateUpdate()
    {
        Vector3 desiredPosition;
        desiredPosition.x = 0;
        desiredPosition.y = target.position.y + offset.y;
        desiredPosition.z = target.position.z + offset.z;
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

}
