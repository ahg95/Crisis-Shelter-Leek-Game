using UnityEngine;

[CreateAssetMenu(fileName = "Task Journey", menuName = "Tasks/Task Journey")]
public class TaskJourney : ScriptableObject
{
    public Task[] tasksInOrder;

    private int assignedTaskInt;
    public Task assignedTask;

    /// <summary>
    /// Go 'up' in the list of tasks == progress.
    /// </summary>
    public void Progress()
    {
        assignedTaskInt++;
        assignedTask = tasksInOrder[assignedTaskInt];
    }
}
