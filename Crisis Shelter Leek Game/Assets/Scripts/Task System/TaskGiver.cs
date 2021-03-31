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

                playerTasks.AssignTask(Instantiate(taskToAssign));
            }
            tasksAssigned = true;
        }
    }
}
