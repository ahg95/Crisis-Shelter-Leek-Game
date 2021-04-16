using UnityEngine;

public class AssignTaskToPlayer : MonoBehaviour
{
    public void AssignTask(Task taskToAssign)
    {
        TaskJourney currentTask = GameObject.FindGameObjectWithTag("Player").GetComponent<TaskJourney>();

        currentTask.assignedTask = Instantiate(taskToAssign);
        // print("task " + taskToAssign.taskID + " assigned!");

        GlobalStats.SaveTask(taskToAssign);

        // Update GUI
        // GameObject currentAction = GameObject.Find("CurrentActionText");
        // GameObject currentActionLocation = GameObject.Find("TaskLocation");

        // currentAction.GetComponent<TMPro.TextMeshProUGUI>().text = taskToAssign.description;
        // currentActionLocation.GetComponent<TMPro.TextMeshProUGUI>().text = taskToAssign.location.ToString();
    }

}