using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class OpenCabinet : MonoBehaviour
{
    [Range(0.5f, 2f)]
    [SerializeField] private float rotationSpeed = 0.75f;
    [SerializeField] private float targetAngle = -90f;
    private bool rotating = false;

    private void OnEnable()
    {
        GetComponent<Interactable>().onInteraction.AddListener(RotateCabinet);
    }
    public void RotateCabinet()
    {
        if (!rotating)
        {
            StartCoroutine(Rotate(targetAngle));
            targetAngle *= -1;
        }
    }

    private IEnumerator Rotate(float targetAngle)
    {
        rotating = true;

        float startTime = Time.time;
        Quaternion startRotation = transform.localRotation;
        Quaternion targetRotation = Quaternion.AngleAxis(targetAngle, Vector3.up) * transform.localRotation;

        while (transform.localRotation != targetRotation)
        {
            float timeSinceStarted = Time.time - startTime;
            float progress = timeSinceStarted / rotationSpeed;

            transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, progress);

            yield return null;
        }

        rotating = false;
    }
}