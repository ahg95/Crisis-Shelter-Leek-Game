using UnityEngine;

public class TryPerformTask : MonoBehaviour
{
    public void SetTasksAsCompletedInCaseTheConditionsAreFulFilled(Task taskToPerform)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerTasks playerTasks = player.GetComponent<PlayerTasks>();

        // Try to find the assigned task in the player's assigned tasks.
        foreach (Task assignedTask in playerTasks.assignedTasks)
        {
            if (assignedTask.taskID == taskToPerform.taskID)
            {
                Task playerTask = assignedTask;

                if (CheckIfAllTheConditionsForTheTaskAreMet(playerTask))
                {
                    playerTask.taskCompleted = true;
                    GlobalStats.IncreaseStats(playerTask.amountOfDays, playerTask.amountOfDays * 100);
                    GameObject.Find("TaskChecker").GetComponent<TaskCompleted>().CheckTaskCompleted(true);
                    // Debug.Log("Task " + playerTask.taskID + " Completed = " + playerTask.taskCompleted);
                    // Transfer scene showing stats, going back to map scene
                    player.GetComponentInChildren<Transitions>().SimpleTransitionStats(true, "MergingSystemsNextScene");
                    player.GetComponentInChildren<UpdateStats>().ShowStats();
                }
                else
                {
                    Debug.Log("You have not met all conditions to fulfill this task!");
                }
            }
        }

        bool CheckIfAllTheConditionsForTheTaskAreMet(Task taskToCheck)
        {
            // The tasks inside the playerAssigned tasks contain the info whether the condition is met or not.
            PlayerTasks playerTaskList = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTasks>();

            bool conditionsMet = true;

            // check each condition task in the tasks' conditiontask(s).
            // for each condition task, look if the player has completed that conditiontask.
            // if any of them returns true (== there is a condition not met), return false == not all conditions are met.
            foreach (Task conditionTask in taskToCheck.conditionTasks)
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
}
