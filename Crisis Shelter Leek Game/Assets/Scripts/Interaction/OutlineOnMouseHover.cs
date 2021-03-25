using UnityEngine;

[RequireComponent(typeof(Outline))]
public class OutlineOnMouseHover : MonoBehaviour
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
}
