using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform camTransform;

    private void Start()
    {
        camTransform = Camera.main.transform;
    }
    void Update()
    {
        Vector3 camPos = camTransform.position;
        camPos.y = transform.position.y; // Make the arrow only rotate towards the player horizontally.

        transform.LookAt(camPos);
    }
}
