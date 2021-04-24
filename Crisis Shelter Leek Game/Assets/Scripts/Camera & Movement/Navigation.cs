using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Navigation : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] Camera cam;
    /// <summary>
    /// Whatever surface is a navigation static and is within the player's vision, it can move towards.
    /// </summary>
    private void Start()
    {
        arrow = Instantiate(arrow);
        arrow.SetActive(false);
    }
    private void Update()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, 15f);

        if (hit.collider != null && hit.collider.gameObject.layer == 10)
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();

            if (Input.GetMouseButton(0))
            {
                agent.SetDestination(hit.point);
                arrow.SetActive(false);

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
    }
}
