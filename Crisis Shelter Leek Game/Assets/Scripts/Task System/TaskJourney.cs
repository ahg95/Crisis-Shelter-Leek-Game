using UnityEngine;

[CreateAssetMenu(fileName = "Task Journey", menuName = "Tasks/Task Journey")]
public class TaskJourney : ScriptableObject, ISerializationCallbackReceiver
{
    [Space(15)]
    public Task assignedTask;
    [SerializeField] private int assignedTaskIndex = 0;

    [Space(5)]
    [SerializeField] private bool resetTask = true;

    [Space(20)]
    public Task[] tasksInOrder;

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
    public void ProgressIfCurrentTask(Task conditionTask)
    {
        if (assignedTaskIndex != tasksInOrder.Length - 1 && assignedTask == conditionTask) // if not the last task
        {
            assignedTaskIndex++;
            assignedTask = tasksInOrder[assignedTaskIndex];
        }
    }

    private void Reset()
    {
        if (resetTask)
        {
            assignedTaskIndex = 0;
            assignedTask = tasksInOrder[assignedTaskIndex];
        }
    }
}
