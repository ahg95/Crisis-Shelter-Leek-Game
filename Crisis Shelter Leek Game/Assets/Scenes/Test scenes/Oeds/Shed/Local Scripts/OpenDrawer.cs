﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class OpenDrawer : MonoBehaviour
{
    public enum DrawerDirection
    {
        Right,
        Left,
        Forwards,
        Backwards
    }
    public DrawerDirection directionToOpenIn = DrawerDirection.Right;
    private int isOpen = 1;
    private bool isMoving = false;
    private void OnEnable()
    {
        GetComponent<Interactable>().onInteraction.AddListener(RotateDrawer);
    }
   
    public void RotateDrawer()
    {
        if (!isMoving)
        {
            StartCoroutine(SmoothMove(transform.position + (GetDirectionToOpenIn() * isOpen * 0.7f * GetComponent<Renderer>().bounds.size.x), 0.05f));
            isOpen *= -1;
        }

        Vector3 GetDirectionToOpenIn()
        {
            switch (directionToOpenIn)
            {
                case DrawerDirection.Right:
                    return Vector3.right;
                case DrawerDirection.Left:
                    return Vector3.left;
                case DrawerDirection.Forwards:
                    return Vector3.forward;
                case DrawerDirection.Backwards:
                    return Vector3.back;
                default:
                    return Vector3.forward;
            }
        }
    }

    IEnumerator SmoothMove(Vector3 target, float delta)
    {
        // Will need to perform some of this process and yield until next frames
        isMoving = true;
        float closeEnough = 0.01f;
        float distance = (transform.position - target).magnitude;

        // GC will trigger unless we define this ahead of time
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        // Continue until we're there
        while (distance >= closeEnough)
        {
            // Confirm that it's moving
            //Debug.Log("Executing Movement");

            // Move a bit then  wait until next  frame
            transform.position = Vector3.Slerp(transform.position, target, delta);
            yield return wait;

            // Check if we should repeat
            distance = (transform.position - target).magnitude;
        }

        // Complete the motion to prevent negligible sliding
        transform.position = target;

        // Confirm  it's ended
        //Debug.Log("Movement Complete");
        isMoving = false;
    }
}