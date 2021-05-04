using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

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
    [SerializeField] private bool debugAlwaysVisible = false;
    [SerializeField] private bool fullCube = false;

    private Outline outline;
    private Camera cam;

    //zooming variables
    [SerializeField] private bool zoom = false;
    [SerializeField] private bool moveTowards = false;

    public bool isZooming = false;
    private NavMeshAgent agent;
    public float zoomAmount;
    private Quaternion camDefaultAngle;
    private GameObject camerarot;

    public bool isSelected = false;

    public virtual void Start()
    {
        agent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
        cam = Camera.main;
        camDefaultAngle = Quaternion.Euler(cam.transform.localRotation.eulerAngles);
        camerarot = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CanvasGroup>().gameObject;
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }
    public virtual void InteractWith()
    {

         onInteraction.Invoke();
        
        if (zoom)
        {
            isSelected = true;
            isZooming = !isZooming;
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
        float distance = Vector3.Distance(transform.position, cam.transform.position);
        zoomAmount = Mathf.RoundToInt(Mathf.Lerp(40, 25, Mathf.Clamp01(distance)));

        if (moveTowards)
        {
            float angle = Vector3.Angle(transform.position, (cam.transform.position - transform.position).normalized); //calculate at which angle you're interacting with the object

            if (angle <= 45)
            {
                camerarot.SetActive(false);
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomAmount, Time.deltaTime * 5);
                cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, Quaternion.LookRotation(transform.position - cam.transform.position), 5 * Time.deltaTime);
            }
            else if (angle > 45)
            {
                camerarot.SetActive(false);
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
            camerarot.SetActive(false);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomAmount, Time.deltaTime * 5);
            cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, Quaternion.LookRotation(transform.position - cam.transform.position), 5 * Time.deltaTime);
        }

    }
    public void ZoomOut()
    {
        camerarot.SetActive(true);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 60, Time.deltaTime * 5);
        cam.transform.localRotation = Quaternion.Slerp(cam.transform.localRotation, camDefaultAngle, 5 * Time.deltaTime);
        if (cam.transform.localRotation == camDefaultAngle) { isSelected = false; }

    }

    public void OnMouseEnter()
    {
        float distance = Vector3.Distance(cam.transform.position, transform.position);
        if (distance < minimumInteractionDistance)
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
}
