using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class MoveToAndTrigger : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform destination;
    [SerializeField] private UnityEvent response;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        StartCoroutine(WaitForDestinationReached(destination));
    }

    private IEnumerator WaitForDestinationReached(Transform destination)
    {
        agent.SetDestination(destination.position);
        animator.SetBool("isWalking", true);

        if (agent.pathPending) // need to check for this, otherwise the while loop  might return true, because the path hadn't been calculated yet.
        {
            yield return null;
        }
        while (agent.remainingDistance > 0.1f)
        {
            yield return new WaitForFixedUpdate();
        }

        animator.SetBool("isWalking", false);
        response.Invoke();
        // When destination reached, switch scene.
    }
}
