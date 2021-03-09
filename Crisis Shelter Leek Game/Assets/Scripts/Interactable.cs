using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour
{
    private Outline outline;
    [SerializeField]
    private float outlineThickness = 8f;

    private void Start()
    {
        outline = GetComponent<Outline>();
        // Initiation
        outline.OutlineColor = Color.yellow;
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
}
