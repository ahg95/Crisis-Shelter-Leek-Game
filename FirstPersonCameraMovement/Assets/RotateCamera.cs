using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private Transform cameraTransform;
    public float rotationLimit = 45f;
    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }
    public void MoveCamera(float rotationSpeed)
    {
        // don't rotate if currentrotation <= 360-45 and negative rotation
        float currentRotation = cameraTransform.transform.localEulerAngles.y;
        float rotation = GetRotation(currentRotation);
        print(rotation);
        if (Mathf.Abs(rotation) <= 45)
        {
            cameraTransform.Rotate(Vector3.up * rotationSpeed);
        }
        else if (rotation <= -45 && rotationSpeed > 0 || rotation >= 45 && rotationSpeed < 0)
        {
            cameraTransform.Rotate(Vector3.up * rotationSpeed);
        }
    }
    private float GetRotation(float currentRotation)
    {
        float returnRot = 0;
        if (currentRotation <= 360 && currentRotation > 46)
        {
            returnRot = currentRotation - 360;
        }
        else
        {
            returnRot = currentRotation;
        }
        return returnRot;
    }
}
