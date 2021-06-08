using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] public float rotationSpeedMultiplier = 1f;
    [SerializeField] private Camera cam = null;
    [SerializeField] private GameObject uiForRotation;
    private bool disableRotation;

    [Range(5f, 30f)]
    [Tooltip("The amount of degrees the player can rotate in.")]
    [Header("Rotation Limit")]
    public int verticalLimits = 15;

    private void Start()
    {
        if (!Application.isEditor)
        Cursor.lockState = CursorLockMode.Confined;
    }
    /// <summary>
    /// Is called on Button Hover
    /// </summary>
    /// <param name="rotationSpeed"></param>
    public void RotateFree(float rotationSpeed)
    {
        if (!disableRotation)
        {
            rotationSpeed *= rotationSpeedMultiplier;

            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
        }
    }

    public void MoveVerticalLimited(float rotation)
    {
        if (!disableRotation)
        {
            rotation *= rotationSpeedMultiplier;

            float rawXRot = cam.transform.localEulerAngles.x;
            float currentXRot = NicerRot();
            // negative rotation == going up and vice versa
            // If the current rotation is lower than the vertical limit and I'm trying to rotate up
            if (currentXRot <= verticalLimits && rotation > 0 || currentXRot >= -verticalLimits & rotation < 0) // if above upper limit && rotating up
            {
                cam.transform.Rotate(Vector3.right * rotation * Time.deltaTime);
            }

            float NicerRot()
            {
                if (rawXRot < 360 && rawXRot > 180)
                {
                    float negativeRot = rawXRot - 360;
                    return negativeRot;
                }
                else
                {
                    return rawXRot;
                }
            }
        }
    }

    public void DisableRotation(bool onOrOff)
    {
        disableRotation = onOrOff;
    }
    private void OnEnable()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
    }
}
