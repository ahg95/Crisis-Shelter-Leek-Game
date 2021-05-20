using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class InteractWith : MonoBehaviour
{
    [Tooltip("The lower this value, the more zoom is possible")]
    [SerializeField] private float minimumFOV = 25f;
    [Tooltip("The higher this value, the less zoom is possible")]
    [SerializeField] private float maxFOV = 40f;
    [HideInInspector] public float zoomAmount;
    [Header("Rotation Speed")]
    [SerializeField] private float horizontalRotationSpeed = 1.5f;
    [SerializeField] private float verticalRotationSpeed = 0.75f;

    [Space(10)]
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask clickableLayer;
    [SerializeField] private GameObject rotateCameraCanvas;
    [SerializeField] private Navigation navComponent = null;
    [SerializeField] private NavMeshAgent agent;


    private Interactable interactableInteractedWith;
    private bool rotatingHorizontally = false;
    private bool rotatingVertically = false;
    private bool zooming = false;
    private bool zoomedIn = false;

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
        StartCoroutine(routine: RotateHorizontally(interactable));
    }    
    /// <summary>
    /// Look at- and invoke the onInteraction event.
    /// </summary>
    /// <param name="interactable"></param>
    public void LookAtAndInvoke(Interactable interactable)
    {
        StartCoroutine(routine: RotateHorizontally(interactable));
        StartCoroutine(routine: WaitForLookAtToInvoke(interactable));
    }
    #region Zooming
    public void ZoomCamOut()
    {
        StartCoroutine(routine: ZoomOut());
    }

    public IEnumerator ZoomIn()
    {
        zooming = true;
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

        zoomedIn = true;
        zooming = false;

        interactableInteractedWith.isSelected = true;
        interactableInteractedWith.zoomedInOn = true;
        cam.fieldOfView = zoomAmount; // When close enough to zoomamount, snap to zoomamount.
    }
    public IEnumerator ZoomOut()
    {
        if (zoomedIn)
        {
            zooming = true;

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

            zoomedIn = false;
            zooming = false;
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

    /// <summary>
    /// Calculates the horizontal angle between the parent object with the navmeshagent and the transform it should face towards, then rotate towards it until facing it.
    /// </summary>
    /// <param name="transformToLookAt"></param>
    /// <returns></returns>
    private IEnumerator RotateHorizontally(Interactable interactable)
    {
        rotatingVertically = true;
        Vector3 localTarget = transform.InverseTransformPoint(interactable.transform.position); // Get the relative local transform position
        float horizontalTargetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg; // Calculate the angle and convert to degrees. Math stuff...  Only works 2D (Which means only on 1 axis I believe?)
       
        if (zooming)
        {
            //Debug.Log("waiting until zooming is finished");
            yield return new WaitForFixedUpdate();
        }

        while ((int)horizontalTargetAngle != 0)
        {
            //Debug.Log("Rotating Horizontally!");

            transform.Rotate(Vector3.up, Mathf.Sign(horizontalTargetAngle), Space.Self);
            localTarget = transform.InverseTransformPoint(interactable.transform.position);
            horizontalTargetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

            yield return new WaitForFixedUpdate();
        }

        rotatingHorizontally = false;
        //Debug.Log("Finished Horizontal Rotation!");

        StartCoroutine(routine: RotateVertically(interactable));
    }

    /// <summary>
    /// Rotates the camera vertically in the direction of the transform to look at
    /// </summary>
    /// <param name="transformToLookAt"></param>
    IEnumerator RotateVertically(Interactable interactable)
    {
        rotatingVertically = true;
        Transform cameraTransform = cam.transform; // Vertical rotation is done with the camera.

        Vector3 localTarget = cameraTransform.InverseTransformPoint(interactable.objectTransformToLookAt.position); // Get the relative local transform position
        float verticalTargetAngle = Mathf.Atan2(localTarget.y, localTarget.z) * Mathf.Rad2Deg; // Calculate the angle and convert to degrees. Math stuff...  Only works 2D (Which means only on 1 axis I believe?)

        if (zooming)
        {
            //Debug.Log("waiting until zooming is finished");
            yield return new WaitForFixedUpdate();
        }

        while ((int)verticalTargetAngle != 0)
        {
            //Debug.Log("Rotating Vertically!");

            cameraTransform.Rotate(Vector3.right, -Mathf.Sign(verticalTargetAngle), Space.Self);
            localTarget = cameraTransform.InverseTransformPoint(interactable.objectTransformToLookAt.position);
            verticalTargetAngle = Mathf.Atan2(localTarget.y, localTarget.z) * Mathf.Rad2Deg;

            yield return new WaitForFixedUpdate();
        }

        rotatingVertically = false;
        //Debug.Log("Finished Vertical Rotation!");

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
        while (rotatingHorizontally || rotatingVertically)
        {
            //print("Waiting until looking at");
            yield return new WaitForFixedUpdate();
        }

        //print("Looking at!");

        interactable.onInteraction.Invoke();
    }
}
