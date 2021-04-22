using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Navigation : MonoBehaviour
{
    /// <summary>
    /// Whatever surface is a navigation static and is within the player's vision, it can move towards.
    /// </summary>

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 15f))
            {
                NavMeshAgent agent = GetComponent<NavMeshAgent>();
                if (!agent.hasPath && hit.collider.gameObject.layer == 10) // if not already moving and clicking on the floor
                {
                    agent.SetDestination(hit.point);
                }
            }
        }
    }
}
