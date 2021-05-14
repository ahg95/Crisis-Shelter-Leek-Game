using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] float rotationSpeedMultiplier = 1f;
    [SerializeField] private Camera cam;

    [Range(5f, 45f)]
    [Tooltip("The amount of degrees the player can rotate in.")]
    [Header("Rotation Limit")]
    public int verticalLimit = 15;
    /// <summary>
    /// Is called on Button Hover
    /// </summary>
    /// <param name="rotationSpeed"></param>
    public void RotateFree(float rotationSpeed)
    {
        rotationSpeed *= rotationSpeedMultiplier;

        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }

    public void MoveVerticalLimited(float rotationSpeed)
    {
        rotationSpeed *= rotationSpeedMultiplier;

        float currentRotation = cam.transform.localEulerAngles.x;
        float rotation = GetRotation(currentRotation);

        if (Mathf.Abs(rotation) <= verticalLimit)
        {
            cam.transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        }
        else if (rotation <= -verticalLimit && rotationSpeed > 0 || rotation >= verticalLimit && rotationSpeed < 0)
        {
            cam.transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        }
    }
    private float GetRotation(float currentRotation)
    {
        if (currentRotation <= 360 && currentRotation > verticalLimit + 1)
        {
            return currentRotation - 360;
        }
        else
        {
            return currentRotation;
        }
    }

    private void OnEnable()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
    }
}
