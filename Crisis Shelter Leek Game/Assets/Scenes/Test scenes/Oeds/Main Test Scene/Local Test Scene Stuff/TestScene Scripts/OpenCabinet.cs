using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class OpenCabinet : MonoBehaviour
{
    [SerializeField]
    private float targetAngle = -90f;
    public enum OpeningAxis
    {
        Horizontal,
        Vertical
    }
    [SerializeField] private OpeningAxis openingAxis = OpeningAxis.Vertical;
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
            transform.Rotate(GetDirectionToOpenIn(), 1f * Mathf.Sign(targetAngle));
            yield return null;
        }

        yield return null;
    }

    private Vector3 GetDirectionToOpenIn()
    {
        switch (openingAxis)
        {
            case OpeningAxis.Horizontal:
                return Vector3.right;
            case OpeningAxis.Vertical:
                return Vector3.up;
            default:
                return Vector3.up;
        }
    }
}