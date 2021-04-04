using UnityEngine;

public class TryPerformTask : MonoBehaviour
{
    /// <summary>
    /// If the player has the task and its conditions are met, it will be completed.
    /// </summary>
    /// <param name="taskToPerform"></param> The task that should be attempted to be performed.
    public void SetTasksAsCompletedInCaseTheConditionsAreFulFilled(Task taskToPerform)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Task assignedTask = player.GetComponent<CurrentTask>().assignedTask;

        // Try to find the assigned task in the player's assigned tasks.
        if (assignedTask.taskID == taskToPerform.taskID)
        {
            if (CheckIfAllTheConditionsForTheTaskAreMet(assignedTask))
            {
                assignedTask.taskCompleted = true;

                GlobalStats.IncreaseStatsManual(assignedTask.amountOfDays, assignedTask.amountOfDays * 100);
                GlobalStats.SaveTask(assignedTask);

                // GameObject.Find("TaskChecker").GetComponent<TaskCompleted>().CheckTaskCompleted(true);

/*                // Transfer scene showing stats, going back to map scene
                player.GetComponentInChildren<Transitions>().SimpleTransitionStats(true, "MergingSystemsNextScene");
                player.GetComponentInChildren<UpdateStats>().ShowStats();*/
            }
            else
            {
                Debug.Log("You have not met all conditions to fulfill this task!");
            }
        }

        bool CheckIfAllTheConditionsForTheTaskAreMet(Task taskToCheck)
        {
            // The tasks inside the playerAssigned tasks contain the info whether the condition is met or not.
            bool conditionsMet = true;

            // check each condition task in the tasks' conditiontask(s).
            // if any of them returns true (== there is a condition not met), return false == not all conditions are met.
            foreach (Task conditionTask in taskToCheck.conditionTasks)
            {
                if (conditionTask.taskID == assignedTask.taskID && !assignedTask.taskCompleted)
                {
                    conditionsMet = false;
                }
            }

            return conditionsMet;
        }
    }
}
