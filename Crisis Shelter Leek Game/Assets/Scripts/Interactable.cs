using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour
{
    private Outline outline;
    [Header("Outline")]
    [SerializeField]
    private float outlineThickness = 8f;
    [SerializeField]
    private Color outlineColor = Color.white;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        // Initiation
        outline.OutlineColor = outlineColor;
        outline.OutlineWidth = outlineThickness;
        outline.enabled = false;
    }
    private void OnMouseEnter()
    {
        outline.enabled = true;
    }
    private void OnMouseExit()
    {
        outline.enabled = false;
    }
    public virtual void InteractWith()
    {
        print("Interacting with interactable object!");
    }
}
