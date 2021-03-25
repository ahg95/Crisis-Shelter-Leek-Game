using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Always make a sprite face the camera.
    void Update()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
    }
}
