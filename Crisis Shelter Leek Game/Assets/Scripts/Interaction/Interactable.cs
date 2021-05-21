using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(Outline))]
[DisallowMultipleComponent]
public class Interactable : MonoBehaviour
{
    [Header("Action when interacting")]
    public UnityEvent onInteraction;
    [Tooltip("Minimum distance the player needs to be in before interaction is possible")]
    [SerializeField] private float minimumInteractionDistance = 5f;

    [Header("Zoom in & Walk Towards?")]
    [SerializeField] public Transform objectTransformToLookAt = null;
    [SerializeField] public bool zoomIn = false;
    [HideInInspector] public bool zoomedInOn = false;
    [HideInInspector] public float zoomAmount;
    [Tooltip("The lower this value, the more zoom is possible")]
    [SerializeField] private float minimumFOV = 25f;
    [Tooltip("The higher this value, the less zoom is possible")]
    [SerializeField] private float maxFOV = 40f;
    [Space(5)]
    [SerializeField] public bool moveTowards = false;
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
        float distance = Vector3.Distance(cam.transform.position, transform.position);
        if (distance < minimumInteractionDistance)
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
        print("interacted through InteractWith Void");
        onInteraction.Invoke();
       /* if (!isSelected)
        {
            if (moveTowards)
            {
                rotateCameraCanvas.SetActive(false);
                navComponent.enabled = false;
                agent.SetDestination(transform.position + transform.forward * 2);

                StartCoroutine(routine: WaitForDestinationReached());
            }
            else if (zoomIn && !moveTowards)
            {
                rotateCameraCanvas.SetActive(false);
                StartCoroutine(routine: RotateTowardsInteractable());
            }
            else
            {
                onInteraction.Invoke();
            }
        }
        else
        {
            rotateCameraCanvas.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(routine: ZoomOut());
        }*/
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
