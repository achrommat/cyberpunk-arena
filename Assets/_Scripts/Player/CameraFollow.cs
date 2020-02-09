using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private float distance;
    [SerializeField] private float distance2;
    [SerializeField] private float smoothSpeed;

    private void FixedUpdate()
    {
        Transform aimingOffsetPoint = player.aimingCameraOffsetPoint;
        Vector3 desiredPosition = new Vector3(aimingOffsetPoint.position.x, aimingOffsetPoint.position.y + distance, aimingOffsetPoint.position.z + distance2);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
