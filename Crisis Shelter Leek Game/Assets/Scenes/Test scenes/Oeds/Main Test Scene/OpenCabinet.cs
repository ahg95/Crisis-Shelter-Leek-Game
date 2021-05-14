using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class OpenCabinet : MonoBehaviour
{
    [SerializeField]
    private float targetAngle = -90f;

    private void OnEnable()
    {
        GetComponent<Interactable>().onInteraction.AddListener(RotateCabinet);
    }
    public void RotateCabinet()
    {
        StartCoroutine(Rotate(targetAngle));
        targetAngle *= -1;
    }

    IEnumerator Rotate(float targetAngle)
    {
        float relativeAngle = 0;
        while (relativeAngle < Mathf.Abs(targetAngle))
        {
            relativeAngle += 1f;
            transform.Rotate(Vector3.up, 1f * Mathf.Sign(targetAngle));
            yield return null;
        }

        yield return null;
    }
}