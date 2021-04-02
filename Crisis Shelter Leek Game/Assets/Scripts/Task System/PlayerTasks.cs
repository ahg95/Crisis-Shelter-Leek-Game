using System.Collections.Generic;
using UnityEngine;

public class PlayerTasks : MonoBehaviour
{
    public List<Task> assignedTasks = new List<Task>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// If the player has the task and its conditions are met, it will be completed.
    /// </summary>
    /// <param name="taskToPerform"></param> The task that should be attempted to be performed.
    public void SetTasksAsCompletedInCaseTheConditonsAreFulFilled(Task taskToPerform)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerTasks playerTasks = player.GetComponent<PlayerTasks>();

        // Try to find the assigned task in the player's assigned tasks.
        foreach (Task assignedTask in playerTasks.assignedTasks)
        {
            if (assignedTask.taskID == taskToPerform.taskID)
            {
                Task playerTask = assignedTask;

                if (CheckIfConditionsMet(playerTask))
                {
                    playerTask.taskCompleted = true;
                    GlobalStats.newAmountOfDays += playerTask.amountOfDays; // add days to stats
                    GlobalStats.newCost += GlobalStats.newAmountOfDays * 100;
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

        bool CheckIfConditionsMet(Task taskToCheck)
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
