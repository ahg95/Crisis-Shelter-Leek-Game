using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

[RequireComponent(typeof(Outline))]
[DisallowMultipleComponent]
public class Interactable : MonoBehaviour
{
    #region Variables
    [Header("Action when interacting")]
    public UnityEvent onInteraction;
    [Tooltip("Minimum distance the player needs to be in before interaction is possible")]

    [Header("Zoom in & Walk Towards?")]
    public Transform objectTransformToLookAt = null;
    public bool zoomIn = false;
    [HideInInspector] public float zoomAmount;
    [Space(5)]
    public bool moveTowards = false;
    public float moveTowardsDistance = 2f;
    public enum FrontOfObject
    {
        Forward,
        Backward,
        Up,
        Down,
        Right,
        Left
    }
    public FrontOfObject frontOfObject = FrontOfObject.Forward;
    [SerializeField] protected Navigation navComponent = null;

    [Space(10)]
    [SerializeField] private Texture2D hoverCursor;

    private Outline outline;
    protected Camera cam;
    protected NavMeshAgent agent;
    protected GameObject rotateCameraCanvas;
    #endregion
    public void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (navComponent == null)
        {
            navComponent = player.GetComponent<Navigation>();
        }

        agent = player.GetComponent<NavMeshAgent>();
        cam = Camera.main;
        rotateCameraCanvas = GameObject.FindGameObjectWithTag("RotationCanvas");
        outline = GetComponent<Outline>();
        outline.enabled = false;

        if (objectTransformToLookAt == null)
        {
            objectTransformToLookAt = transform;
        }
    }
    #region OnMouseStuff
    private void OnMouseEnter()
    {
        if (enabled)
        {
            Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.ForceSoftware);
            outline.enabled = true;
        }
    }
    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        outline.enabled = false;
    }
    #endregion

    public virtual void InteractWith()
    {
        onInteraction.Invoke();
    }
}
