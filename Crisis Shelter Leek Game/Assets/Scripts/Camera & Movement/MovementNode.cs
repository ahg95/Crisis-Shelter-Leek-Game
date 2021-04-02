using UnityEngine;
public class MovementNode : Interactable
{
    [SerializeField] private bool debug = false;
    [Space(10)]
    public MovementNode[] connectedNodes;
    private MoveToNode Movement;
    public override void Start()
    {
        base.Start();
    }
    public override void InteractWith()
    {
        Movement = Camera.main.transform.parent.GetComponent<MoveToNode>();

        if (!Movement.isMoving)
        {
            VisibleNodes visibleNodesManager = transform.parent.GetComponent<VisibleNodes>();
            visibleNodesManager.SetNewCurrentNode(GetComponent<MovementNode>());

            Movement.MoveTowardsNode(transform);
        }
    }


    private void OnDrawGizmos()
    {
        if (Application.isPlaying && debug)
        {
            Gizmos.color = Color.yellow;

            foreach (MovementNode node in connectedNodes)
            {
                Gizmos.DrawLine(transform.position, node.transform.position);
            }

            // Show forward direction
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, transform.forward * 5);
        }
    }
}
