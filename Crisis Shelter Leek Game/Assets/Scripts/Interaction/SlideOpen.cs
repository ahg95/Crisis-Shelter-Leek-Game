using System.Collections;
using UnityEngine;

public class SlideOpen : Interactable
{
    private int isOpen = 1;
    private bool isMoving = false;
    public override void InteractWith()
    {
        base.InteractWith();
        if (!isMoving)
        {
            StartCoroutine(SmoothMove(transform.position + (Vector3.right * isOpen * 0.7f * GetComponent<Renderer>().bounds.size.x), 0.05f));
            isOpen *= -1;
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
            Debug.Log("Executing Movement");

            // Move a bit then  wait until next  frame
            transform.position = Vector3.Slerp(transform.position, target, delta);
            yield return wait;

            // Check if we should repeat
            distance = (transform.position - target).magnitude;
        }

        // Complete the motion to prevent negligible sliding
        transform.position = target;

        // Confirm  it's ended
        Debug.Log("Movement Complete");
        isMoving = false;
    }
}
