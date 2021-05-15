using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    // ADD: Cursor change on hover

    [Header("Action when interacting")]
    public UnityEvent onInteraction;
    [Tooltip("Minimum distance the player needs to be in before interaction is possible")]
    [SerializeField] private float minimumInteractionDistance = 5f;
    private bool withinInteractionDistance = false;

    [Header("Zoom in & Walk Towards?")]
    [SerializeField] private bool zoom = false;
    [SerializeField] private bool moveTowards = false;
    [HideInInspector] public bool isZooming = false;
    [HideInInspector] public float zoomAmount;
    [HideInInspector] public bool isSelected = false;

    [Header("Debug")]
    [Space(10)]
    [SerializeField] private bool debugAlwaysVisible = false;
    [SerializeField] private bool fullCube = false;

    [Space(10)]
    [SerializeField] private Texture2D hoverCursor;

    protected Vector3 centerOfMesh;
    private Camera cam;
    [HideInInspector] public NavMeshAgent agent;
    private Quaternion camDefaultAngle;
    private GameObject camRot;

    public virtual void Start()
    {
        centerOfMesh = GetComponentInChildren<MeshRenderer>().bounds.center;
        agent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
        cam = Camera.main;
        camDefaultAngle = Quaternion.Euler(cam.transform.localRotation.eulerAngles);
        camRot = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CanvasGroup>().gameObject;
    }
    public virtual void InteractWith()
    {
        if (withinInteractionDistance)
        {
            onInteraction.Invoke();

            if (zoom)
            {
                isSelected = true;
                isZooming = !isZooming;
            }
        }
    }
    private void Update()
    {
        if (zoom && isSelected)
        {
            if (isZooming)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }
    }

    /// <summary>
    /// if move towards is true 
    /// </summary>
    public void ZoomIn()
    {
        float distance = Vector3.Distance(centerOfMesh, cam.transform.position);
        zoomAmount = Mathf.RoundToInt(Mathf.Lerp(40, 25, Mathf.Clamp01(distance)));

        if (moveTowards)
        {
            float angle = Vector3.Angle(transform.position, (cam.transform.position - transform.position).normalized); //calculate at which angle you're interacting with the object

            if (angle <= 45)
            {
                camRot.SetActive(false);
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomAmount, Time.deltaTime * 5);
                cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, Quaternion.LookRotation(transform.position - cam.transform.position), 5 * Time.deltaTime);
            }
            else if (angle > 45)
            {
                camRot.SetActive(false);
                agent.SetDestination(transform.position + transform.forward * 2);
                if (!agent.hasPath)//wait until he has reached the destination to rotate
                {
                    cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, Quaternion.LookRotation(transform.position - cam.transform.position), 5 * Time.deltaTime);

                    if (cam.transform.rotation == Quaternion.LookRotation(transform.position - cam.transform.position))//wait until rotation has finished to zoom in
                    {
                        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomAmount, Time.deltaTime * 5);
                    }
                }
            }
        }
        else
        {
            camRot.SetActive(false);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomAmount, Time.deltaTime * 5);
            cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, Quaternion.LookRotation(transform.position - cam.transform.position), 5 * Time.deltaTime);
        }

    }
    public void ZoomOut()
    {
        camRot.SetActive(true);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 60, Time.deltaTime * 5);
        cam.transform.localRotation = Quaternion.Slerp(cam.transform.localRotation, camDefaultAngle, 5 * Time.deltaTime);
        if (cam.transform.localRotation == camDefaultAngle) { isSelected = false; }

    }
    public void OnMouseEnter()
    {
        float distance = Vector3.Distance(cam.transform.position, centerOfMesh);
        if (distance < minimumInteractionDistance)
        {
            Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.ForceSoftware);

            withinInteractionDistance = true;
        }
        else
        {
            withinInteractionDistance = false;
        }
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

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
            Gizmos.DrawWireCube(centerOfMesh, Vector3.one * minimumInteractionDistance * 2);
        }
        else
        {
            Gizmos.DrawCube(centerOfMesh, Vector3.one * minimumInteractionDistance * 2);
        }
    }
}
