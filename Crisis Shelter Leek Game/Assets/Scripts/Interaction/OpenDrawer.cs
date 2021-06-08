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
    [SerializeField] private float openingPercentage = 0.7f;
    [SerializeField] private float openingSpeed = 0.75f;
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
            StartCoroutine(SmoothMove());
        }
    }

    private IEnumerator SmoothMove()
    {
        isMoving = true;

        float startTime = Time.time;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = transform.position + GetDirectionToOpenIn() * isOpen * GetComponent<Renderer>().bounds.size.x * openingPercentage;

        while (transform.position != targetPosition)
        {
            float timeSinceStarted = Time.time - startTime;
            float progress = timeSinceStarted / openingSpeed;

            transform.position = Vector3.Lerp(startPosition, targetPosition, progress);

            yield return new WaitForFixedUpdate();
        }

        isMoving = false;
        isOpen *= -1;

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
}