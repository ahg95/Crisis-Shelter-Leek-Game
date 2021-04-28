using UnityEngine;

public class SmoothRotationWithLimits : MonoBehaviour
{
    [SerializeField] float rotationSpeedMultiplier = 1f;
    [Range(5f, 179f)] [Tooltip("The amount of degrees the player can rotate in.")]
    public int rotationLimitAngle = 90;
    /// <summary>
    /// Is called on Button Hover
    /// </summary>
    /// <param name="rotationSpeed"></param>
    public void MoveCameraVertical(float rotationSpeed)
    {
        rotationSpeed *= rotationSpeedMultiplier;

        float currentRotation = transform.localEulerAngles.x;
        float rotation = GetRotation(currentRotation);

        if (Mathf.Abs(rotation) <= rotationLimitAngle)
        {
            transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        }
        else if (rotation <= -rotationLimitAngle && rotationSpeed > 0 || rotation >= rotationLimitAngle && rotationSpeed < 0)
        {
            transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
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
