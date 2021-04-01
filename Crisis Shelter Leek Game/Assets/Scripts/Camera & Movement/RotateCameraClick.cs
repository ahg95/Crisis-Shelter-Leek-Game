﻿using UnityEngine;

public class RotateCameraClick : MonoBehaviour
{
    public int rotationStrength = 45;
    public float turningRate = 3f;
    private bool isTurning = false;
    private Vector3 targetRotation;

    // -1 = left, 1 = right
    public void Turn(int direction)
    {
        if (!isTurning)
        {
            //transform.Rotate(Vector3.up * (direction * rotationStrength));
            //print(transform.localRotation.eulerAngles);
            float yRot = transform.localRotation.eulerAngles.y;
            print(direction);
            print(yRot);
            float roundedYRot = Mathf.RoundToInt(yRot);
            if ((roundedYRot >= 360 - rotationStrength && direction == 1) || (roundedYRot <= rotationStrength && direction == -1) || (roundedYRot == 0 || roundedYRot == 360)) // limits the amount of rotation the player can do
            {
                targetRotation = new Vector3(transform.localRotation.eulerAngles.x, Mathf.RoundToInt(yRot + direction * rotationStrength), transform.localRotation.eulerAngles.z);
                isTurning = true;
            }
        }
    }
    private void Update()
    {
        if (isTurning)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(targetRotation), Time.deltaTime * turningRate);
            if (RoundedRot(transform.localRotation) == RoundedRot(Quaternion.Euler(targetRotation)))
            {
                isTurning = false;
            }
        }
    }
    private Vector3 RoundedRot(Quaternion rotation)
    {
        Vector3 VectRot = rotation.eulerAngles;
        Vector3 roundedVect = new Vector3(Mathf.RoundToInt(VectRot.x), Mathf.RoundToInt(VectRot.y), Mathf.RoundToInt(VectRot.z));
        return roundedVect;
    }
}

