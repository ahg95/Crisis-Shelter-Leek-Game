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

    [Header("Rotation Speed")]
    [SerializeField] private float rotationSpeed = 1f;
    private bool rotating = false;

    [Header("Instantiating")]
    [Space(10)]
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask clickableLayer;
    [SerializeField] private Navigation navComponent = null;
    [SerializeField] private NavMeshAgent agent;

    private Interactable interactedObject;

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
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, clickableLayer))
            {
                GameObject clickedObject = hit.collider.gameObject;

                if (!alreadySelectedInteractable(clickedObject.GetComponent<Interactable>()))
                {
                    interactedObject = clickedObject.GetComponent<Interactable>();

                    if (interactedObject.moveTowards) // MOVE TO OBJECT
                    {
                        StartCoroutine(MoveTowards(interactedObject));
                    }
                    else if (interactedObject.zoomIn && !interactedObject.moveTowards) // ZOOM IN ON INTERACTABLE
                    {
                        LookAtAndInvoke(interactedObject);
                    }
                    else // immmediately invoke
                    {
                        interactedObject.InteractWith();
                    }
                }
                else
                {
                    Deselect(); // Deselect if clicking on object that is already focused on
                }
            }
            else
            {
                Deselect();
            }

            bool alreadySelectedInteractable(Interactable interactable)
            {
                if (interactedObject != null)
                {
                    if (interactedObject.gameObject.GetInstanceID() == interactable.gameObject.GetInstanceID())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
    }

    private void Deselect()
    {
        StopAllCoroutines();
        StartCoroutine(ZoomOut());
        if (interactedObject)
        {
            interactedObject.isSelected = false;
            interactedObject.zoomedInOn = false;
            interactedObject = null;
        }
    }

    /// <summary>
    /// Look at without invoking the onInteraction Event.
    /// </summary>
    /// <param name="interactable"></param>
    public void LookAt(Interactable interactable)
    {
        interactedObject = interactable;
        StartCoroutine(RotateTo(interactable));
    }    

    /// <summary>
    /// Look at- and invoke the onInteraction event.
    /// </summary>
    /// <param name="interactable"></param>
    public void LookAtAndInvoke(Interactable interactable)
    {
        StartCoroutine(RotateTo(interactable));
        StartCoroutine(WaitForLookAtToInvoke(interactable));
    }
    #region Zooming
    public void ZoomCameraOut()
    {
        StartCoroutine(ZoomOut());
    }

    public IEnumerator ZoomIn(Interactable interactable)
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

        interactable.isSelected = true;
        interactable.zoomedInOn = true;
        cam.fieldOfView = zoomAmount; // When close enough to zoomamount, snap to zoomamount.
    }
    public IEnumerator ZoomOut()
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
    }
    #endregion

    /// <summary>
    /// Sets off a set of functions that will first move in front of the interactable, then rotate towards it to face it, then invoke the interaction.
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveTowards(Interactable interactable)
    {
        agent.SetDestination(interactable.transform.position + GetFront(interactable.transform) * interactedObject.moveTowardsDistance);

        if (agent.pathPending) // need to check for this, otherwise the while loop  might return true, because the path hadn't been calculated yet.
        {
            yield return null;
        }

        while (agent.remainingDistance > 0.05f)
        {
            yield return new WaitForFixedUpdate();
        }

        LookAtAndInvoke(interactedObject); // Rotate towards the interactable when the destination is reached.

        Vector3 GetFront (Transform interactableTransform)
        {
            switch (interactable.frontOfObject)
            {
                case Interactable.FrontOfObject.Forward:
                    return interactableTransform.forward;
                case Interactable.FrontOfObject.Right:
                    return interactableTransform.right;
                case Interactable.FrontOfObject.Up:
                    return interactableTransform.up;
                default:
                    return interactableTransform.forward;
            }
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
            // print("Rotating");
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
            StartCoroutine(ZoomIn(interactable));
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

        //print("Looking at!");
        interactable.isSelected = true;
        interactable.InteractWith();
    }
}