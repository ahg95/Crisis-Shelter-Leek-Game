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
    [SerializeField] private float minimumInteractionDistance = 5f;

    [Space(15)]
    [SerializeField] private bool alwaysVisible = false;

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
            print("Interacting");
            onInteraction.Invoke();
        }
    }

    public void OnMouseEnter()
    {
        float distance = Vector3.Distance(cam.transform.position, transform.position);
        if (distance < minimumInteractionDistance)
        {
            print(distance);
            hovering = true;
            outline.enabled = true;
        }
    }
    public void OnMouseExit()
    {
        hovering = false;
        outline.enabled = false;
    }

    private void OnDrawGizmos()
    {
        if (alwaysVisible)
        {
            Draw();
        }
    }

    public void OnDrawGizmosSelected()
    {
        Draw();
    }

    void Draw()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one * minimumInteractionDistance * 2);
    }
}
