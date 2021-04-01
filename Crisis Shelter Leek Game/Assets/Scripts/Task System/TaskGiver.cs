using UnityEngine;

public class TaskGiver : MonoBehaviour
{
    [Tooltip("Drag in the tasks you want to assign to the player here.")]
    [SerializeField] private Task[] tasksToAssign;
    [Space(20)]
    [SerializeField] private bool tasksAssigned = false;

    /// <summary>
    /// When called, the player's assigned task list is retrieved and the tasks that need to be assigned, which are dragged into the serialized list above, are assigned.
    /// </summary>
    public void AssignTasks()
    {
        PlayerTasks playerTasks = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTasks>();

        if (!tasksAssigned)
        {
            for (int i = 0; i < tasksToAssign.Length; i++)
            {
                Task taskToAssign = tasksToAssign[i];

                playerTasks.assignedTasks.Add(Instantiate(taskToAssign));
                print("task " + taskToAssign.taskID + " assigned!");

            }
            tasksAssigned = true;

            // Update GUI
            GameObject currentAction = GameObject.Find("CurrentTask");
            GameObject currentActionLocation = GameObject.Find("TaskLocation");

            currentAction.GetComponent<TMPro.TextMeshProUGUI>().text = tasksToAssign[0].description;
            currentActionLocation.GetComponent<TMPro.TextMeshProUGUI>().text = tasksToAssign[0].location.ToString();
        }
    }
}