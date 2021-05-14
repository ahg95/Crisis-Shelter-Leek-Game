using UnityEngine;

public class CrouchCamera : MonoBehaviour
{
    [SerializeField] float crouchAmount = -0.5f;
    [SerializeField] float crouchSpeed = 0.5f;
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (transform.localPosition.y > crouchAmount)
            {
                Vector3 position = transform.localPosition;
                position.y -= Time.deltaTime / crouchSpeed;
                transform.localPosition = position;
            }
        }
        else
        {
            if (transform.localPosition.y < 0f)
            {
                Vector3 position = transform.localPosition;
                position.y += Time.deltaTime / crouchSpeed;
                transform.localPosition = position;
            }
        }
    }
}
