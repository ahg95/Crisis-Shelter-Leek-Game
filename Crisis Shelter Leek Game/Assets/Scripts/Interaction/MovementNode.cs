using System.Collections.Generic;
using UnityEngine;
public class MovementNode : Interactable
{
    [Space(10)]
    [SerializeField]
    private MovementNode[] connectedNodes;
    private MoveToNode Movement;
    private Camera cam;
    private void Start()
    {
        cam = Camera.main;
        Movement = cam.transform.parent.GetComponent<MoveToNode>();
        //gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f, 0.5f, 0.5f);
    }
    public override void InteractWith()
    {
        if (!Movement.isMoving)
        {
            NodesManager nodesManager = transform.parent.parent.GetComponent<NodesManager>();
            base.InteractWith();
            // You click on a node
            // All the other nodes that you could see turn invisible
            NodeVisibility(false, nodesManager.visibleNodes);
            nodesManager.visibleNodes.Clear();

            // You move towards the node you clicked
            Movement.MoveTowardsNode(transform);

            // When you arrive at the node you clicked, you see the nodes where you can go from there.
            foreach (MovementNode node in connectedNodes)
            {
                nodesManager.visibleNodes.Add(node);
            }
            NodeVisibility(true, nodesManager.visibleNodes);
        }
    }

    private void NodeVisibility(bool visibility, List<MovementNode> nodesToSet)
    {
        foreach (MovementNode node in nodesToSet)
        {
            node.gameObject.SetActive(visibility);
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.yellow;

            foreach (MovementNode node in connectedNodes)
            {
                Gizmos.DrawLine(transform.position, node.transform.position);
            }
        }

        // Show forward direction
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * 5);
    }
}
