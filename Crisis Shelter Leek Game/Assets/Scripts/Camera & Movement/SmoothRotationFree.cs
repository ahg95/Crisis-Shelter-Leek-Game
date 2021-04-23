using UnityEngine;

public class SmoothRotationFree : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float rotationSpeedMultiplier = 1f;

    /// <summary>
    /// Is called on Button Hover
    /// </summary>
    /// <param name="rotationSpeed"></param>
    public void MoveCameraHor(float rotationSpeed)
    {
        rotationSpeed *= rotationSpeedMultiplier;

        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
    public void MoveCameraVer(float rotationSpeed)
    {
        rotationSpeed *= rotationSpeedMultiplier;

        transform.Rotate(transform.right * rotationSpeed * Time.deltaTime, Space.World);
    }
}
