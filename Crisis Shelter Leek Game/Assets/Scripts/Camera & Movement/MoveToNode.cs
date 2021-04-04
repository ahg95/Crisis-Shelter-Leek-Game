using System.Collections;
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
            // Camera.main.transform.localRotation = Quaternion.Euler(Vector3.zero);
            StartCoroutine(IMoveRotate(Quaternion.Euler(Vector3.zero)));
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
    private IEnumerator IMoveRotate(Quaternion targetRotation)
    {
        // Keep track of how far we've travelled along this path.
        Transform cam = Camera.main.transform;
        float progress = 0;

        // Keep our original position
        Quaternion originalRotation = cam.localRotation;

        // Default transition time is 1
        while (progress < 3f)
        {
            float t = progress / 3f;
            cam.localRotation = Quaternion.Lerp(originalRotation, targetRotation, t);

            progress += 0.05f;

            yield return null;
        }
        cam.localRotation = targetRotation;
        isMoving = false;
    }
}
