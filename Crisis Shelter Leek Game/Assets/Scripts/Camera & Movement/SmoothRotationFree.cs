using UnityEngine;

public class SmoothRotationFree : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float rotationSpeedMultiplier = 1f;

    /// <summary>
    /// Is called on Button Hover
    /// </summary>
    /// <param name="rotationSpeed"></param>
    public void MoveCamera(float rotationSpeed)
    {
        rotationSpeed *= rotationSpeedMultiplier;

        playerTransform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
}
