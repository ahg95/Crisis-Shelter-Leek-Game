using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class InteractWith : MonoBehaviour
{
    [Header("Zooming")]
    [Tooltip("The lower this value, the more zoom is possible")]
    [SerializeField] private float minimumFOV = 25f;
    [Tooltip("The higher this value, the less zoom is possible")]
    [SerializeField] private float maxFOV = 40f;
    [HideInInspector] public float zoomAmount;
    private bool zoomedIn = false;

    [Header("Rotation Speed")]
    [SerializeField] private float rotationSpeed = 1f;
    private bool rotating = false;

    [Header("Instantiating")]
    [Space(10)]
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask clickableLayer;
    [SerializeField] private GameObject rotateCameraCanvas;
    [SerializeField] private Navigation navComponent = null;
    [SerializeField] private NavMeshAgent agent;


    private Interactable interactableInteractedWith;

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

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, clickableLayer))
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
                        LookAtAndInvoke(interactableInteractedWith);
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

    /// <summary>
    /// Look at without invoking the onInteraction Event.
    /// </summary>
    /// <param name="interactable"></param>
    public void LookAt(Interactable interactable)
    {
        interactableInteractedWith = interactable;
        StartCoroutine(routine: RotateTo(interactable));
    }    
    /// <summary>
    /// Look at- and invoke the onInteraction event.
    /// </summary>
    /// <param name="interactable"></param>
    public void LookAtAndInvoke(Interactable interactable)
    {
        StartCoroutine(RotateTo(interactable));
        //StartCoroutine(routine: RotateHorizontally(interactable));
        StartCoroutine(routine: WaitForLookAtToInvoke(interactable));
    }
    #region Zooming
    public void ZoomCamOut()
    {
        StartCoroutine(routine: ZoomOut());
    }

    public IEnumerator ZoomIn()
    {
        float distance = Vector3.Distance(transform.position, cam.transform.position);
        zoomAmount = Mathf.RoundToInt(Mathf.Lerp(40, 25, Mathf.Clamp01(distance)));

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

        zoomedIn = true;
        interactableInteractedWith.isSelected = true;
        interactableInteractedWith.zoomedInOn = true;
        cam.fieldOfView = zoomAmount; // When close enough to zoomamount, snap to zoomamount.
    }
    public IEnumerator ZoomOut()
    {
        if (zoomedIn)
        {
            float timeStartedZooming = Time.time;
            float startingFOV = cam.fieldOfView;
            float targetFOV = 60;

            while (cam.fieldOfView < targetFOV)
            {
                float timeSinceStartedZooming = Time.time - timeStartedZooming;
                float percentageCompleted = timeSinceStartedZooming / 0.5f; // how fast the zoom out and rotation-correction goes

                cam.fieldOfView = Mathf.Lerp(startingFOV, targetFOV, percentageCompleted);

                yield return null;
            }

            // print("zoomed out!");
            navComponent.enabled = true;
            zoomedIn = false;
            interactableInteractedWith.isSelected = false;
            interactableInteractedWith = null;
        }
    }

    /*public IEnumerator RotateTowardsInteractable(Interactable interactable)
    {
        Vector3 lookAtPos = interactable.objectTransformToLookAt.position;
        Vector3 directionToObject = lookAtPos - cam.transform.position;

        float timeStartedRotating = Time.time;
        Quaternion startingAngle = cam.transform.rotation;
        Quaternion targetAngleCam = Quaternion.LookRotation(lookAtPos - cam.transform.position);

        while (Vector3.Angle(cam.transform.forward, directionToObject) > 0.1f) // VECTOR IS NOT WORKING
        {
            print(Vector3.Angle(cam.transform.forward, directionToObject));

            float timeSinceStarted = Time.time - timeStartedRotating;
            float percentageCompleted = timeSinceStarted / 0.75f;

            cam.transform.rotation = Quaternion.Slerp(startingAngle, targetAngleCam, percentageCompleted);
            yield return null;
        }

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
    }*/
    #endregion

    /// <summary>
    /// Sets off a set of functions that will first move in front of the interactable, then rotate towards it to face it, then invoke the interaction.
    /// </summary>
    /// <returns></returns>
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
            LookAtAndInvoke(interactableInteractedWith); // Rotate towards the interactable when the destination is reached.
        }
    }

    private IEnumerator RotateTo(Interactable interactable)
    {
        rotating = true;
        // print("Rotate");
        // Horizontal position of interactable
        Vector3 horizontalTargetPos = new Vector3(interactable.transform.position.x, transform.position.y, interactable.objectTransformToLookAt.position.z);

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(horizontalTargetPos - transform.position);

        Quaternion camStartRotation = cam.transform.localRotation; // stamp of startRotation camera

        // Calculate the angle: Opposite distance / adjacent distance
        float directionMultiplier = -Mathf.Sign(interactable.objectTransformToLookAt.position.y - cam.transform.position.y); // Makes the angle positive when the player should look down, negative when up
        float angleOnY = directionMultiplier * Mathf.Atan(Vector3.Distance(horizontalTargetPos, interactable.objectTransformToLookAt.position) / Vector3.Distance(cam.transform.position, horizontalTargetPos)) * Mathf.Rad2Deg;
        // Quaternion.angleaxis == give an angle, will give you back the right quaternion to rotate to.
        Quaternion camTargetRotation = Quaternion.AngleAxis(angleOnY, Vector3.right); // rotate on the local x Axis

        float timeStamp = Time.time;

        while (Quaternion.Angle(transform.rotation, targetRotation) > 1 || Quaternion.Angle(cam.transform.localRotation, camTargetRotation) > 1) // "close enough angles"
        {
            print("Rotating");
            float timeSinceStarted = Time.time - timeStamp;
            float progression = timeSinceStarted / rotationSpeed;

            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, progression);
            cam.transform.localRotation = Quaternion.Slerp(camStartRotation, camTargetRotation, progression);

            yield return new WaitForFixedUpdate();
        }

        rotating = false;
        //print("Finished rotating!");

        if (interactable.zoomIn)
        {
            StartCoroutine(routine: ZoomIn());
        }
    }

    /// <summary>
    /// Invoked to check whether the player is still rotating on any axis, then invoke onINteraction when it is no longer.
    /// </summary>
    /// <param name="interactable"></param>
    /// <returns></returns>
    private IEnumerator WaitForLookAtToInvoke(Interactable interactable)
    {
        while (rotating)
        {
            // print("Waiting until looking at");
            yield return new WaitForFixedUpdate();
        }

        // print("Looking at!");
        interactable.isSelected = true;
        interactable.onInteraction.Invoke();
    }
}
