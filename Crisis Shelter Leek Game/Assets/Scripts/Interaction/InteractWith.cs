using UnityEngine;

public class InteractWith : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask clickableLayer;

    private void Start()
    {
        cam = Camera.main;
        clickableLayer = 1 << LayerMask.NameToLayer("Clickable");
    }
    /// <summary>
    /// If you click something which is on the clickable layer, get the Interactable class from that clickable and execute InteractWith.
    /// </summary>
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableLayer))
            {
                hit.collider.GetComponentInParent<Interactable>().InteractWith();
            }
        }
    }


}
