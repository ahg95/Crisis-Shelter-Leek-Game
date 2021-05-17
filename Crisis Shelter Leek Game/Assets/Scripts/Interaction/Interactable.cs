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
    [SerializeField] private Transform objectTransformToLookAt = null;
    [SerializeField] protected bool zoomIn = false;
    [HideInInspector] public bool isZooming = false;
    [HideInInspector] public float zoomAmount;
    [Tooltip("The lower this value, the more zoom is possible")]
    [SerializeField] private float minimumFOV = 25f;
    [Tooltip("The higher this value, the less zoom is possible")]
    [SerializeField] private float maxFOV = 40f;
    [Space(5)]
    [SerializeField] protected bool moveTowards = false;
    [SerializeField] private Navigation navComponent = null;

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
    protected RotateCamera rotateCameraComponent;

    public void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (navComponent == null)
        navComponent = player.GetComponent<Navigation>();
        agent = player.GetComponent<NavMeshAgent>();
        cam = Camera.main;
        rotateCameraComponent = player.GetComponent<RotateCamera>();
        outline = GetComponent<Outline>();
        outline.enabled = false;

        if (objectTransformToLookAt == null)
        {
            objectTransformToLookAt = transform;
        }
    }
    public virtual void InteractWith()
    {
        if (!isSelected)
        {
            if (moveTowards)
            {
                rotateCameraComponent.enabled = false;
                agent.SetDestination(transform.position + transform.forward * 2);

                StartCoroutine(routine: WaitForDestinationReached());
            }
            else if (zoomIn && !moveTowards)
            {
                rotateCameraComponent.enabled = false;
                StartCoroutine(routine: RotateTowardsInteractable());
            }
            else
            {
                onInteraction.Invoke();
            }
        }
        else
        {
            rotateCameraComponent.enabled = true;
            StopAllCoroutines();
            StartCoroutine(routine: ZoomOut());
        }
    }

    #region Zooming
    public IEnumerator ZoomIn()
    {
        float distance = Vector3.Distance(transform.position, cam.transform.position);
        zoomAmount = Mathf.RoundToInt(Mathf.Lerp(maxFOV, minimumFOV, Mathf.Clamp01(distance)));

        float timeStartedZooming = Time.time;
        float startFOV = cam.fieldOfView;
        float targetFOV = zoomAmount;

        while (cam.fieldOfView > zoomAmount)
        {
            isZooming = true;
            float timeSinceStartedZooming = Time.time - timeStartedZooming;
            float percentageCompleted = timeSinceStartedZooming / 0.75f; // zoom in speed
            cam.fieldOfView = Mathf.Lerp(startFOV, targetFOV, percentageCompleted);
            yield return null;
        }

        isSelected = true;
        cam.fieldOfView = zoomAmount; // When close enough to zoomamount, snap to zoomamount.
    }

    public IEnumerator ZoomOut()
    {
        // variables for looking the 'normal' forward direction.
        Vector3 objectPos = objectTransformToLookAt.position;
        objectPos.y = cam.transform.position.y;
        Vector3 targetDirection = (objectPos - cam.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion startrotation = cam.transform.rotation;

        float timeStartedZooming = Time.time;
        float startingFOV = cam.fieldOfView;
        float targetFOV = 60;

        while (cam.fieldOfView < targetFOV || Quaternion.Angle(cam.transform.rotation, targetRotation) > 0.01f)
        {
            float timeSinceStartedZooming = Time.time - timeStartedZooming;
            float percentageCompleted = timeSinceStartedZooming / 0.5f; // how fast the zoom out and rotation-correction goes

            cam.transform.rotation = Quaternion.Slerp(startrotation, targetRotation, percentageCompleted); // lerp rotation

            cam.fieldOfView = Mathf.Lerp(startingFOV, targetFOV, percentageCompleted);

            yield return null;
        }
        // 'Reset' values: needs to be done because the yRotation of the player is handled by the parent object, while the horizontal rotation is done by the camera. 
        // This is necessary because of how the navMeshAgent works.
        cam.transform.parent.LookAt(objectPos);
        cam.transform.LookAt(objectPos);

        isSelected = false;
    }
    public IEnumerator RotateTowardsInteractable()
    {
        Vector3 directionToObject = objectTransformToLookAt.position - cam.transform.position;

        // Smooth and precise lerping
        float timeStartedRotating = Time.time;
        Quaternion startingAngle = cam.transform.rotation;
        Quaternion targetAngleCam = Quaternion.LookRotation((objectTransformToLookAt.position - cam.transform.position));

        while (Vector3.Angle(cam.transform.forward, directionToObject) > 0.1f)
        {
            float timeSinceStarted = Time.time - timeStartedRotating;
            float percentageCompleted = timeSinceStarted / 0.75f;

            cam.transform.rotation = Quaternion.Slerp(startingAngle, targetAngleCam, percentageCompleted);
            yield return null;
        }

        // Zoom in if this is enabled.
        if (zoomIn)
        {
            StartCoroutine(routine: ZoomIn());
        }
        else
        {
            isSelected = true;
        }
    }
    #endregion

    #region Movement Towards
    /// <summary>
    /// Moves player towards the object, then turn towards it.
    /// </summary>
    private IEnumerator WaitForDestinationReached()
    {
        if (agent.pathPending) // need to check for this, otherwise the while loop  might return true, because the path hadn't been calculated yet.
        {
            yield return null;
        }
        while (agent.remainingDistance > 0.1f)
        {
            //print("moving towards destination");
            yield return null;
        }

        if (agent.remainingDistance < 0.1f)
        {
            //print("Reached destination!");
            StartCoroutine(RotateTowardsInteractable()); // Rotate towards the interactable when the destination is reached.
        }
    }
    #endregion

    #region OnMouseStuff
    private void OnMouseEnter()
    {
        float distance = Vector3.Distance(cam.transform.position, transform.position);
        if (distance < minimumInteractionDistance)
        {
            Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.ForceSoftware);

            outline.enabled = true;
            navComponent.enabled = false;
        }
    }
    private void OnMouseExit()
    {
        navComponent.enabled = true;

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        outline.enabled = false;
    }
    #endregion

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
