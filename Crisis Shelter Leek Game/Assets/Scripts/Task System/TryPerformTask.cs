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
        Task assignedTask = player.GetComponent<TaskJourney>().assignedTask;

        // Try to find the assigned task in the player's assigned tasks.
        if (assignedTask.taskID == taskToPerform.taskID)
        {

            assignedTask.taskCompleted = true;

            GlobalStats.IncreaseStatsManual(assignedTask.amountOfDays, assignedTask.amountOfDays * 100);
            GlobalStats.SaveTask(assignedTask);

            // GameObject.Find("TaskChecker").GetComponent<TaskCompleted>().CheckTaskCompleted(true);

            /*                // Transfer scene showing stats, going back to map scene
                            player.GetComponentInChildren<Transitions>().SimpleTransitionStats(true, "MergingSystemsNextScene");
                            player.GetComponentInChildren<UpdateStats>().ShowStats();*/
        }
    }
}
