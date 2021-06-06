using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(Outline))]
[DisallowMultipleComponent]
public class Interactable : MonoBehaviour
{
    #region variables
    [Header("Action when interacting")]
    public UnityEvent onInteraction;
    [Tooltip("Minimum distance the player needs to be in before interaction is possible")]
    [SerializeField] private float minimumInteractionDistance = 5f;

    [Header("Zoom in & Walk Towards?")]
    public Transform objectTransformToLookAt = null;
    public bool zoomIn = false;
    [HideInInspector] public bool zoomedInOn = false;
    [HideInInspector] public float zoomAmount;
    [Space(5)]
    public bool moveTowards = false;
    public float moveTowardsDistance = 2f;
    public enum FrontOfObject
    {
        Forward,
        Up,
        Right,
    }
    public FrontOfObject frontOfObject = FrontOfObject.Forward;
    [SerializeField] protected Navigation navComponent = null;

    public bool isSelected = false;

    [Header("Debug")]
    [Space(10)]
    [SerializeField] private bool debugAlwaysVisible = false;
    [SerializeField] private bool fullCube = false;

    [Space(10)]
    [SerializeField] private Texture2D hoverCursor;

    private Outline outline;
    protected Camera cam;
    protected NavMeshAgent agent;
    protected GameObject rotateCameraCanvas;
    #endregion
    public void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (navComponent == null)
        navComponent = player.GetComponent<Navigation>();
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
 /*       float distance = Vector3.Distance(cam.transform.position, transform.position);
        if (distance < minimumInteractionDistance)
        {*/
            Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.ForceSoftware);

            outline.enabled = true;
        //}
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


    #region Gizmos
    private void OnDrawGizmos()
    {
        if (debugAlwaysVisible)
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

        if (!fullCube)
        {
            Gizmos.DrawWireCube(transform.position, Vector3.one * minimumInteractionDistance * 2);
        }
        else
        {
            Gizmos.DrawCube(transform.position, Vector3.one * minimumInteractionDistance * 2);
        }
    }
    #endregion
}
