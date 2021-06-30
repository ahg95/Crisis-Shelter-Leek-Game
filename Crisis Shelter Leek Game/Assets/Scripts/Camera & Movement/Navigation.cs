using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
public class Navigation : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject arrow;
    [SerializeField] private Camera cam;
    [Header("Audio")]
    [SerializeField] private AudioClip walkingSound;
    [Range(0, 1)]
    [SerializeField] private float walkSoundVolume = 0.375f;
    private AudioSource walkSoundPlayer;
    /// <summary>
    /// Whatever surface is a navigation static and is within the player's vision, it can move towards.
    /// </summary>
    private void Start()
    {
        walkSoundPlayer = gameObject.AddComponent<AudioSource>();
        walkSoundPlayer.loop = true;
        walkSoundPlayer.playOnAwake = false;
        walkSoundPlayer.clip = walkingSound;

        arrow = Instantiate(arrow);
        arrow.SetActive(false);
        agent = GetComponent<NavMeshAgent>();

        cam = Camera.main;
    }
    private void Update()
    {
        NavigationRaycast();
    }

    private void NavigationRaycast()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, 15f);

        bool raycastingFloor = RaycastingFloor(hit);

        if (raycastingFloor)
        {
            arrow.SetActive(true);
            Cursor.visible = false;

            if (Input.GetMouseButton(0))
            {
                agent.SetDestination(hit.point);
                StartCoroutine(WaitForDestinationReached());
            }

            Vector3 position = transform.position;
            position.y = arrow.transform.position.y;
            arrow.transform.LookAt(position, Vector3.up);
            arrow.transform.position = hit.point;
        }
        else // if not hovering over the floor
        {
            arrow.SetActive(false);
            Cursor.visible = true;
        }

        bool RaycastingFloor(RaycastHit raycastHit)
        {
            bool rayOnFloor = false;

            if (raycastHit.collider != null && !EventSystem.current.IsPointerOverGameObject())
                rayOnFloor = raycastHit.collider.gameObject.layer == LayerMask.NameToLayer("Floor");

            return rayOnFloor;
        }
    }

    private IEnumerator WaitForDestinationReached()
    {
        walkSoundPlayer.volume = walkSoundVolume;
        walkSoundPlayer.Play();

        if (agent.pathPending) // need to check for this, otherwise the while loop  might return true, because the path hadn't been calculated yet.
        {
            //print("Path Pending");
            yield return null;
        }
        while (agent.remainingDistance > 0.1f)
        {
            //print("moving towards destination");
            yield return new WaitForFixedUpdate();
        }

        if (agent.remainingDistance < 0.1f)
        {
            StartCoroutine(LowerVolume());
            
            //print("Reached destination!");
        }
    }
    private IEnumerator LowerVolume()
    {
        float totalTime = 0.6f; // fade audio out over 3 seconds
        float currentTime = 0;

        while (walkSoundPlayer.volume > 0)
        {
            currentTime += Time.deltaTime;
            walkSoundPlayer.volume = Mathf.Lerp(walkSoundVolume, 0, currentTime / totalTime);
            yield return null;
        }

        walkSoundPlayer.Pause();
    }
}
