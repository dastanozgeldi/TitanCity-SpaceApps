using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform playerTransform;
    public Transform roverTransform;
    public float smoothSpeedX;
    public float smoothSpeedY;
    public float smoothSpeedZ;

    Vector3 targetPosition;
    private Vector3 offset;

    public float rotationSpeed;

    private void Start()
    {
        offset = transform.position - playerTransform.position;
    }

    private void FixedUpdate()
    {
        /*targetPosition = playerTransform.position + offset;

        float smoothX = Mathf.Lerp(transform.position.x, targetPosition.x, smoothSpeedX * Time.fixedDeltaTime);
        float smoothY = Mathf.Lerp(transform.position.y, targetPosition.y, smoothSpeedY * Time.fixedDeltaTime);
        float smoothZ = Mathf.Lerp(transform.position.z, targetPosition.z, smoothSpeedZ * Time.fixedDeltaTime);

        Vector3 smoothedPosition = new Vector3(smoothX, smoothY, smoothZ);
        transform.position = smoothedPosition;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up * mouseX * rotationSpeed);
        transform.Rotate(Vector3.up * mouseY * rotationSpeed);*/

    }
}
