using UnityEngine;

public class InteractWith : MonoBehaviour
{
    private Camera cam;
    private LayerMask clickableLayer;

    private void Start()
    {
        cam = Camera.main;
        clickableLayer = 1 << LayerMask.NameToLayer("Clickable");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableLayer))
            {
                //hit.collider.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                hit.collider.GetComponentInParent<Interactable>().InteractWith();
            }
        }
    }
}
