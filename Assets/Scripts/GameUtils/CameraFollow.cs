using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cameraParent;
    public Transform target;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    public void FixedUpdate()
    {
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 7.2f, -9.1f));

        cameraParent.position = Vector3.SmoothDamp(cameraParent.position, targetPosition, ref velocity, smoothTime);
    }
}
