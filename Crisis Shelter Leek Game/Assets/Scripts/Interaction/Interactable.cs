using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour
{
    // ADD: Cursor change on hover

    bool hovering = false;
    [Header("Action when interacting")]
    public UnityEvent onInteraction;
    [Tooltip("Minimum distance the player needs to be in before interaction is possible")]
    [SerializeField] private float minimumDistance = 5f;

    private Outline outline;
    private Camera cam;

    public virtual void Start()
    {
        cam = Camera.main;
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }
    public virtual void InteractWith()
    {
        if (hovering)
        {
            onInteraction.Invoke();
        }
    }

    public void OnMouseEnter()
    {
        if (Vector3.Distance(cam.transform.position, transform.position) < minimumDistance)
        {
            hovering = true;
            outline.enabled = true;
        }
    }
    public void OnMouseExit()
    {
        hovering = false;
        outline.enabled = false;
    }
}
