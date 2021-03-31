using UnityEngine;

public static class PerformTask
{
    /// <summary>
    /// When this void is triggered, it finds the player and loops through its currently assigned tasks.
    /// If the player has the task and its conditions are met, it will be completed.
    /// </summary>
    /// <param name="task"></param> The task that should be attempted to be performed.
    public static void TryPerformTask(Task task)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerTasks playerTasks = player.GetComponent<PlayerTasks>();
        Task playerTask = null;
        // Try to find the assigned task in the player's assigned tasks.
        foreach (Task assignedTask in playerTasks.assignedTasks)
        {
            if (assignedTask.taskID == task.taskID)
            {
                playerTask = assignedTask;

                if (CheckIfConditionsMet(playerTask))
                {
                    playerTask.taskCompleted = true;
                    Debug.Log("Task " + playerTask.taskID + " Completed = " + playerTask.taskCompleted);
                }
                else
                {
                    Debug.Log("You have not met all conditions to fulfill this task!");
                }
            }
        }

        if (playerTask == null)
        {
            Debug.Log("You don't have this task!");
        }
    }

    /// <summary>
    /// Gets the conditionTasks that are found in the Task contructors for each task that is currently assigned to the player.
    /// </summary>
    /// <returns></returns>
    public static bool CheckIfConditionsMet(Task task)
    {
        // The tasks inside the playerAssigned tasks contain the info whether the condition is met or not.
        PlayerTasks playerTaskList = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTasks>();

        bool conditionsMet = true;

        // check each condition task in the tasks' conditiontask(s).
        // for each condition task, look if the player has completed that conditiontask.
        // if any of them returns true (== there is a condition not met), return false == not all conditions are met.
        foreach (Task conditionTask in task.conditionTasks)
        {
            foreach (Task playerAssignedTask in playerTaskList.assignedTasks)
            {
                if (conditionTask.taskID == playerAssignedTask.taskID && !playerAssignedTask.taskCompleted)
                {
                    conditionsMet = false;
                }
            }
        }

        return conditionsMet;
    }
}
