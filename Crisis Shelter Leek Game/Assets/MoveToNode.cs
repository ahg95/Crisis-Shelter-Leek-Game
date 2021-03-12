using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToNode : MonoBehaviour
{
    [SerializeField]
    private float speed = 6f;
    public bool isMoving = false;
    private Transform targetNode;
    public void MoveTowardsNode(Transform node)
    {
        if (!isMoving)
        {
            targetNode = node;
            isMoving = true;
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            StartCoroutine(IMoveTransform(targetNode.position, targetNode.rotation, speed));

            // transform.position = Vector3.MoveTowards(transform.position, targetNode.position, Time.deltaTime * speed);
            // transform.rotation = Quaternion.RotateTowards(transform.rotation, targetNode.rotation, Time.deltaTime * speed * 5)
        }
    }

    private IEnumerator IMoveTransform(Vector3 targetPosition, Quaternion targetRotation, float speed)
    {
        // Keep track of how far we've travelled along this path.
        float progress = 0;
        float distance = (transform.localPosition - targetPosition).magnitude;

        // Keep our original position
        Vector3 originalPosition = transform.localPosition;
        Quaternion originalRotation = transform.localRotation;

        // Default transition time is 1
        while (progress < distance)
        {
            float t = progress / distance;

            transform.localPosition = Vector3.Lerp(originalPosition, targetPosition, t);
            transform.localRotation = Quaternion.Lerp(originalRotation, targetRotation, t);

            progress += speed * Time.deltaTime;

            yield return null;
        }
        transform.localPosition = targetPosition;
        transform.localRotation = targetRotation;
        isMoving = false;
    }
}
