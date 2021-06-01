using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MoveToShowRoom : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform destination;
    [SerializeField] private Transitions transitions;

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
            //print("Path Pending");
            yield return null;
        }
        while (agent.remainingDistance > 0.1f)
        {
            //print("moving towards destination");
            yield return new WaitForFixedUpdate();
        }

        animator.SetBool("isWalking", false);
        transitions.LoadSimpleSceneTransition("Bedroom Wender With Coach");
        // When destination reached, switch scene.
    }

}
