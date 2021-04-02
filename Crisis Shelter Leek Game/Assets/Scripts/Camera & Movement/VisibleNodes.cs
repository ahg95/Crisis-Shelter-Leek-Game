using System.Collections.Generic;
using UnityEngine;

public class VisibleNodes : MonoBehaviour
{
    public MovementNode currentNode;
    public List<MovementNode> visibleNodes = new List<MovementNode>();

    public void SetNodesVisibility(bool visibility)
    {
        for (int i = 0; i < visibleNodes.Count; i++)
        {
            MovementNode node = visibleNodes[i];
            node.gameObject.SetActive(visibility);
        }
    }

    public void SetNewCurrentNode(MovementNode newNode)
    {
        currentNode = newNode;

        SetNodesVisibility(false);
        visibleNodes.Clear();

        // add new currentnode visible nodes to visible nodes list
        foreach (MovementNode node in currentNode.connectedNodes)
        {
            visibleNodes.Add(node);
        }

        SetNodesVisibility(true);
    }
}
