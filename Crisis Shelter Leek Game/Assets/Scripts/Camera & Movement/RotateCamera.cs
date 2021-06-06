using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] public float rotationSpeedMultiplier = 1f;
    [SerializeField] private Camera cam = null;
    [SerializeField] private GameObject uiForRotation;

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
        rotationSpeed *= rotationSpeedMultiplier;

        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }

    public void MoveVerticalLimited(float rotation)
    {
        rotation *= rotationSpeedMultiplier;
        //print(rotation);

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

       /* rotationSpeed *= rotationSpeedMultiplier;

        float currentRotation = cam.transform.localEulerAngles.x;
        float rotation = GetRotation(currentRotation);

*//*        if (Mathf.Abs(rotation) <= verticalLimit)
        {
            cam.transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        }*//*
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
        }*/
    }

    private void OnEnable()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
    }
}
