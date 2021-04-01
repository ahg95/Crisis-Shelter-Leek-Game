using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private Transform cameraTransform;
    [Range(30f, 179f)] [Tooltip("The amount of degrees the player can rotate in.")]
    public int rotationLimit = 90;
    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }
    public void MoveCamera(float rotationSpeed)
    {
        float currentRotation = cameraTransform.transform.localEulerAngles.y;
        float rotation = GetRotation(currentRotation);
        // print(rotation);
        if (Mathf.Abs(rotation) <= rotationLimit)
        {
            cameraTransform.Rotate(Vector3.up * rotationSpeed);
        }
        else if (rotation <= -rotationLimit && rotationSpeed > 0 || rotation >= rotationLimit && rotationSpeed < 0)
        {
            cameraTransform.Rotate(Vector3.up * rotationSpeed);
        }
    }
    private float GetRotation(float currentRotation)
    {
        if (currentRotation <= 360 && currentRotation > rotationLimit + 1)
        {
            return currentRotation - 360;
        }
        else
        {
            return currentRotation;
        }
    }
}
