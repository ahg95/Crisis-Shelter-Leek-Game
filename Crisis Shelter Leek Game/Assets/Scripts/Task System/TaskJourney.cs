using UnityEngine;

[CreateAssetMenu(fileName = "Task Journey", menuName = "Tasks/Task Journey")]
public class TaskJourney : ScriptableObject, ISerializationCallbackReceiver
{
    #region Variables
    [Space(15)]
    public Task assignedTask;
    [SerializeField] private int assignedTaskIndex = 0;

    [Space(5)]
    [SerializeField] private bool resetTask = true;

    [Space(20)]
    public Task[] tasksInOrder;

    // Days spent at Zienn
    private int currentAmountOfDaysAtWender = 0;
    private int daysSpentAfterProgression = 0;
    #endregion
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
            AddDaysSpent();
            assignedTaskIndex++;
            assignedTask = tasksInOrder[assignedTaskIndex];
        }
    }
    public void AddDaysSpent()
    {
        currentAmountOfDaysAtWender = daysSpentAfterProgression;
        daysSpentAfterProgression += assignedTask.amountOfDays; // Add the amount of days it takes to progress to the next task
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
