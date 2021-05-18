using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class InteractWith : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask clickableLayer;
    [SerializeField] private GameObject rotateCameraCanvas;
    [SerializeField] private Navigation navComponent = null;
    [SerializeField] private NavMeshAgent agent;
    [Space(10)]
    [Tooltip("The lower this value, the more zoom is possible")]
    [SerializeField] private float minimumFOV = 25f;
    [Tooltip("The higher this value, the less zoom is possible")]
    [SerializeField] private float maxFOV = 40f;
    [HideInInspector] public float zoomAmount;

    private Interactable interactableInteractedWith;
    private bool rotatingTowardsObject = false;

    private void Start()
    {
        cam = Camera.main;
        clickableLayer = 1 << LayerMask.NameToLayer("Clickable");
        navComponent = GetComponent<Navigation>();
        agent = GetComponent<NavMeshAgent>();
    }
    /// <summary>
    /// If you click something which is on the clickable layer, get the Interactable class from that clickable and execute InteractWith.
    /// </summary>
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableLayer))
            {
                GameObject interactedObject = hit.collider.gameObject;
                interactableInteractedWith = interactedObject.GetComponent<Interactable>();

                if (!interactableInteractedWith.isSelected)
                {
                    if (interactableInteractedWith.moveTowards) // MOVE TO OBJECT
                    {
                        rotateCameraCanvas.SetActive(false);
                        navComponent.enabled = false;
                        agent.SetDestination(interactedObject.transform.position + interactedObject.transform.forward * 2);
                        StartCoroutine(routine: WaitForDestinationReached());
                    }
                    else if (interactableInteractedWith.zoomIn && !interactableInteractedWith.moveTowards) // ZOOM IN ON INTERACTABLE
                    {
                        rotateCameraCanvas.SetActive(false);
                        StartCoroutine(routine: RotateTowardsInteractable(interactableInteractedWith));
                    }
                    else // immmediately invoke
                    {
                        interactableInteractedWith.onInteraction.Invoke();
                    }
                }
                else // Else, zoom out of interactable
                {
                    rotateCameraCanvas.SetActive(true);
                    StopAllCoroutines();
                    StartCoroutine(routine: ZoomOut());
                }
            }
        }
    }

    public void LookAt(Interactable inter)
    {
        StartCoroutine(routine: RotateTowardsInteractable(inter));
    }
    #region Zooming
    public void ZoomCamOut()
    {
        StartCoroutine(routine: ZoomOut());
    }

    public IEnumerator ZoomIn()
    {
        float distance = Vector3.Distance(transform.position, cam.transform.position);
        zoomAmount = Mathf.RoundToInt(Mathf.Lerp(maxFOV, minimumFOV, Mathf.Clamp01(distance)));

        float timeStartedZooming = Time.time;
        float startFOV = cam.fieldOfView;
        float targetFOV = zoomAmount;

        while (cam.fieldOfView > zoomAmount)
        {
            float timeSinceStartedZooming = Time.time - timeStartedZooming;
            float percentageCompleted = timeSinceStartedZooming / 0.75f; // zoom in speed
            cam.fieldOfView = Mathf.Lerp(startFOV, targetFOV, percentageCompleted);
            yield return null;
        }

        interactableInteractedWith.isSelected = true;
        interactableInteractedWith.zoomedInOn = true;
        interactableInteractedWith.onInteraction.Invoke();
        cam.fieldOfView = zoomAmount; // When close enough to zoomamount, snap to zoomamount.
    }

    public IEnumerator ZoomOut()
    {
        // variables for looking the 'normal' forward direction.
        Vector3 forwardFacingDirection = interactableInteractedWith.objectTransformToLookAt.position;
        forwardFacingDirection.y = cam.transform.position.y;
        Vector3 targetDirection = (forwardFacingDirection - cam.transform.position).normalized;
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
        cam.transform.parent.LookAt(forwardFacingDirection);
        cam.transform.LookAt(forwardFacingDirection);

        interactableInteractedWith.isSelected = false;
        interactableInteractedWith = null;
    }

    public IEnumerator RotateTowardsInteractable(Interactable interactable)
    {
        Vector3 lookAtPos = interactable.objectTransformToLookAt.position;
        Vector3 directionToObject = lookAtPos - cam.transform.position;

        float timeStartedRotating = Time.time;
        Quaternion startingAngle = cam.transform.rotation;
        Quaternion targetAngleCam = Quaternion.LookRotation(lookAtPos - cam.transform.position);
        rotatingTowardsObject = true;

        while (Vector3.Angle(cam.transform.forward, directionToObject) > 0.1f) // VECTOR IS NOT WORKING
        {
            print(Vector3.Angle(cam.transform.forward, directionToObject));

            float timeSinceStarted = Time.time - timeStartedRotating;
            float percentageCompleted = timeSinceStarted / 0.75f;

            cam.transform.rotation = Quaternion.Slerp(startingAngle, targetAngleCam, percentageCompleted);
            yield return null;
        }

        rotatingTowardsObject = false;

        // Zoom in if this is enabled.
        if (interactable.zoomIn)
        {
            StartCoroutine(routine: ZoomIn());
        }
        else
        {
            navComponent.enabled = true;
            rotateCameraCanvas.SetActive(true);
            interactable.onInteraction.Invoke();
            interactable.isSelected = true;
        }
    }
    #endregion

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
            StartCoroutine(RotateTowardsInteractable(interactableInteractedWith)); // Rotate towards the interactable when the destination is reached.
        }
    }
}
