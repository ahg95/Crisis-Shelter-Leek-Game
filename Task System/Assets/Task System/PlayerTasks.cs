using System.Collections.Generic;
using UnityEngine;

public class PlayerTasks : MonoBehaviour
{
    public List<Task> assignedTasks = new List<Task>();

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// This void is called in the Taskgiver.cs script to assign a task to the player's assigned tasks list.
    /// It also updates the GUI accordingly.
    /// </summary>
    /// <param name="task"></param> The task to assign.
    public void AssignTask(Task task)
    {
        print("task " + task.taskID + " assigned!");
        assignedTasks.Add(task);
    }
}
