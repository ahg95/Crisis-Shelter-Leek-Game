using UnityEngine;

[RequireComponent(typeof(Outline))]
public class InteractWith : MonoBehaviour
{
    private Camera cam;
    private LayerMask hitLayer;

    private void Start()
    {
        cam = Camera.main;
        hitLayer = 1 << LayerMask.NameToLayer("Clickable");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, hitLayer))
            {
                //hit.collider.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                if (Vector3.Distance(cam.transform.position, hit.transform.position) < 5f)
                {
                    hit.collider.GetComponentInParent<Interactable>().InteractWith();
                }
            }
        }
    }
}
