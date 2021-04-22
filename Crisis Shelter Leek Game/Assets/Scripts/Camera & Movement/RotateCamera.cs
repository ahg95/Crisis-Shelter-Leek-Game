using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private Transform cameraTransform;
    [SerializeField] float rotationSpeedMultiplier = 1f;
    [Space(15)]
    public bool rotationLimit = false;
    [Range(30f, 179f)] [Tooltip("The amount of degrees the player can rotate in.")]
    public int rotationLimitAngle = 90;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }
    /// <summary>
    /// Is called on Button Hover
    /// </summary>
    /// <param name="rotationSpeed"></param>
    public void MoveCamera(float rotationSpeed)
    {
        rotationSpeed *= rotationSpeedMultiplier;
        float currentRotation = cameraTransform.transform.localEulerAngles.y;
        float rotation = GetRotation(currentRotation);

        if (rotationLimit)
        {
            if (Mathf.Abs(rotation) <= rotationLimitAngle)
            {
                cameraTransform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }
            else if (rotation <= -rotationLimitAngle && rotationSpeed > 0 || rotation >= rotationLimitAngle && rotationSpeed < 0)
            {
                cameraTransform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            cameraTransform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

    }
    private float GetRotation(float currentRotation)
    {
        if (currentRotation <= 360 && currentRotation > rotationLimitAngle + 1)
        {
            return currentRotation - 360;
        }
        else
        {
            return currentRotation;
        }
    }
}
