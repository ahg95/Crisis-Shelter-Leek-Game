using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Task Journey", menuName = "Tasks/Task Journey")]
public class TaskJourney : ScriptableObject, ISerializationCallbackReceiver
{
    [Space(15)]
    public Task assignedTask;

    [Space(20)]
    public Task[] tasksInOrder;

    private int assignedTaskIndex = 0;

    public void OnAfterDeserialize()
    {
        Reset();
    }

    public void OnBeforeSerialize()
    {

    }

    public void Progress()
    {
        if (assignedTaskIndex != tasksInOrder.Length - 1) // if not the last task
        {
            assignedTaskIndex++;
            assignedTask = tasksInOrder[assignedTaskIndex];
        }
    }

    private void Reset()
    {
        assignedTaskIndex = 0;
        assignedTask = tasksInOrder[assignedTaskIndex];
    }
}
