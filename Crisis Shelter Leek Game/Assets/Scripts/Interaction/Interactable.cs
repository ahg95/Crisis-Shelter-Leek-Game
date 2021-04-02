using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour
{
    public UnityEvent onInteraction;

    private Outline outline;
    [Header("Outline")]
    [SerializeField]
    private float outlineThickness = 8f;
    [SerializeField]
    private Color outlineColor = Color.white;
    public Camera cam;
    [SerializeField] private float minimumDistance = 5f;

    public virtual void Start()
    {
        cam = Camera.main;
        outline = GetComponent<Outline>();
        outline.OutlineColor = outlineColor;
        outline.OutlineWidth = outlineThickness;
        outline.enabled = false;
    }
    public virtual void InteractWith()
    {
        onInteraction.Invoke();
        // print("Interacting with interactable object!");
    }

    public void OnMouseEnter()
    {
        if (Vector3.Distance(cam.transform.position, transform.position) < minimumDistance)
        {
            outline.enabled = true;
        }
    }
    public void OnMouseExit()
    {
        outline.enabled = false;
    }
}
