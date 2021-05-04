using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Navigation : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject arrow;
    [SerializeField] Camera cam;
    [SerializeField] AudioClip walkingSound;
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

    }
    private void Update()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, 15f);

        if (hit.collider != null && hit.collider.gameObject.layer == 10)
        {
            if (Input.GetMouseButton(0))
            {
                agent.SetDestination(hit.point);
                arrow.SetActive(false);

                StopAllCoroutines();
                walkSoundPlayer.volume = 1f;
                walkSoundPlayer.Play();
            }

            if (!arrow.activeSelf && !agent.hasPath)
            {
                arrow.SetActive(true);
                Cursor.visible = false;
            }

            Vector3 position = transform.position;
            position.y = arrow.transform.position.y;
            arrow.transform.LookAt(position, Vector3.up);
            arrow.transform.position = hit.point;
        }
        else
        {
            arrow.SetActive(false);
            Cursor.visible = true;
        }

        if (!agent.hasPath && walkSoundPlayer.isPlaying)
        {
            StartCoroutine(LowerVolume());
        }
    }

    IEnumerator LowerVolume()
    {
        float totalTime = 0.6f; // fade audio out over 3 seconds
        float currentTime = 0;

        while (walkSoundPlayer.volume > 0)
        {
            currentTime += Time.deltaTime;
            walkSoundPlayer.volume = Mathf.Lerp(1, 0, currentTime / totalTime);
            yield return null;
        }

        walkSoundPlayer.Pause();
    }
}
