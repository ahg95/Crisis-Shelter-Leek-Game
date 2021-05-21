using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] float rotationSpeedMultiplier = 1f;
    [SerializeField] private Camera cam = null;
    [SerializeField] private GameObject uiForRotation;

    [Range(5f, 30f)]
    [Tooltip("The amount of degrees the player can rotate in.")]
    [Header("Rotation Limit")]
    public int verticalLimit = 15;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
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

/*        if (Mathf.Abs(rotation) <= verticalLimit)
        {
            cam.transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        }*/
        if ((rotation <= verticalLimit && rotationSpeed > 0) // negative rot == looking up > if trying to look down & haven't reached lower limit
            || (rotation >= -verticalLimit && rotationSpeed < 0)) // positive rot == looking down > if trying to look up & haven't reached upper limit
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
