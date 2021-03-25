using UnityEngine;

public class RandomDirectionPush : MonoBehaviour
{
    private Rigidbody2D rgb;
    [SerializeField] private float force = 25f;
    private void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rgb.AddForce(Random.insideUnitCircle * force);
        }
    }
}
