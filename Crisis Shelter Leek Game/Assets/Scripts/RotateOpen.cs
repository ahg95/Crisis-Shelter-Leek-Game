using System.Collections;
using UnityEngine;

public class RotateOpen : Interactable
{
    [SerializeField]
    private float targetAngle = 90f;
    private bool isRunning = false;
    public override void InteractWith()
    {
        base.InteractWith();

        if (!isRunning)
        {
            StartCoroutine(Rotate(targetAngle));
        }
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
