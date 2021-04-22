using UnityEngine;

public class SmoothRotationFree : MonoBehaviour
{
    private Transform cameraTransform;
    [SerializeField] float rotationSpeedMultiplier = 1f;

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

        cameraTransform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
}
