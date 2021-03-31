using System.Collections.Generic;
using UnityEngine;

public class TaskGiver : MonoBehaviour
{
    [SerializeField] private List<Task> tasksToAssign = new List<Task>();
    private PlayerTasks playerTasks;
    private bool tasksAssigned = false;

    private void Start()
    {
        playerTasks = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTasks>();
    }

    public void AssignTasks()
    {
        if (!tasksAssigned)
        {
            for (int i = 0; i < tasksToAssign.Count; i++)
            {
                Task taskToAssign = tasksToAssign[i];

                playerTasks.AssignTask(Instantiate(taskToAssign));
            }
            tasksAssigned = true;
        }
    }
}
