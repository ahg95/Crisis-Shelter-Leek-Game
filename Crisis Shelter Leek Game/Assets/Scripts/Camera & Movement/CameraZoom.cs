using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CameraZoom : MonoBehaviour
{
    /*
      In order for inspecting to work:
      The object you want to inspect needs to have a tag "Inspect" and a collider
    */


    //zoom variables
    private int maxZoomAmount = 15;
    private int minZoomAmount = 40;
    private int zoomAmount;
    private float smooth = 5;

    //ray variables
    [Tooltip("maximum distance the player has to be in order to zoom in to a specific object")]
    public float maxDistance = 2;
    private List<Vector3> hitInfo = new List<Vector3>();//stores the hit information of the raycast

    //boolean checks for zooming and rotating
    public bool isZoomedIn = false;
    public bool isRotating = false;

    //movement and rotation of the camera
    [Tooltip("The speed at which the camera will rotate towards the inspectable object")]
    public float speedRot;
    private Navigation movement;
    private Quaternion defaultAngle; //the angle at which the camera will return to when it's not inspecting an object anymore
    private GameObject cameraRot;



    void Start()
    {
        cameraRot = gameObject.transform.parent.GetComponentInChildren<CanvasGroup>().gameObject;
        movement = gameObject.transform.parent.GetComponent<Navigation>();
        defaultAngle = Quaternion.Euler(this.transform.localRotation.eulerAngles);
    }

    /// <summary>
    /// By clicking on an object with the tag Interact:
    /// >If the angle you're clicking at is within 45 degrees then you can rotate and zoom in on the object right away
    /// >If the angle you're clicking at is bigger than 45 degrees then you willbe moved in front of the object after whihc you will rotate and zoom in
    /// By interacting with the object again your rotation and field of view will be set to normal
    /// </summary>
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float angle; //the angle between the ray and the inspectable object
        NavMeshAgent agent = transform.parent.GetComponent<NavMeshAgent>();
        

        if (Physics.Raycast(ray, out hit, maxDistance) && hit.collider.CompareTag("Inspect"))
        {
            angle = Vector3.Angle(hit.transform.up, (transform.position - hit.point).normalized);
            print(angle);
            movement.enabled = false;
            zoomAmount = Mathf.RoundToInt(Mathf.Lerp(minZoomAmount, maxZoomAmount, Mathf.Clamp01(hit.distance))); //the zoom will depend on the distance between you and the object

            if (Input.GetMouseButtonDown(0) && angle <= 45)
            {
                isZoomedIn = !isZoomedIn;
                isRotating = !isRotating;

                hitInfo.Add(hit.transform.position);
            }
            else if (Input.GetMouseButtonDown(0) && angle >= 45 && !isZoomedIn) //move the player in front of the poster and rotate him towards the poster and zoom in
            {
                hitInfo.Add(hit.transform.position);

                agent.SetDestination(hit.transform.position);
                if (!agent.hasPath) { isRotating = true; }

            }
        }
        else
        {
            movement.enabled = true;
        }

        if (isRotating)
        {
            cameraRot.SetActive(false);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(hitInfo[hitInfo.Count - 1] - transform.position), speedRot * Time.deltaTime);

            //if you're not zooming in at the same time as rotating than wait until the end of the rotation to zoom in
            if (!isZoomedIn && transform.rotation == Quaternion.LookRotation(hitInfo[hitInfo.Count - 1] - transform.position))
            {
                isZoomedIn = true;
            }
        }
        else
        {
            this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, defaultAngle, speedRot * Time.deltaTime);
            hitInfo.Clear();
        }

        if (isZoomedIn)
        {
            cameraRot.SetActive(false);
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoomAmount, Time.deltaTime * smooth);
        }
        else
        {
            cameraRot.SetActive(true);
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, 60, Time.deltaTime * smooth);
        }
    }
}


// if you click on an object with the tag interact:
//  >zoom in on the object
//  >make the zoomed in object the middle of the screen
//  >depending on how close you are to the object zoom in respectively
//  >disable camera rotation


//if it's already zoomed in on an abject dont make it possible to zoom in on another one
//if i'm further away from the object then zoom in more (if dis = 2 then zoom = max)
//if i'm closer to the object don't zoom in too much



//if it's already zoomed in on an object than don't be able to click on another object of that type