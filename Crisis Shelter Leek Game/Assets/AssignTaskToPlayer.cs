using UnityEngine;

public class AssignTaskToPlayer : MonoBehaviour
{
    public void AssignTask(Task taskToAssign)
    {
        PlayerTasks playerTasks = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTasks>();

        playerTasks.assignedTasks.Add(Instantiate(taskToAssign));
        print("task " + taskToAssign.taskID + " assigned!");

        // Update GUI
        GameObject currentAction = GameObject.Find("CurrentActionText");
        GameObject currentActionLocation = GameObject.Find("TaskLocation");

        currentAction.GetComponent<TMPro.TextMeshProUGUI>().text = taskToAssign.description;
        currentActionLocation.GetComponent<TMPro.TextMeshProUGUI>().text = taskToAssign.location.ToString();
    }
}