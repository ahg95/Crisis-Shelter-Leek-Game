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

    [Space(5)]
    public GameEvent taskProgressionEvent;

    [Space(20)]
    public Task[] tasksInOrder;

    public int CurrentAmountOfDaysAtWender { get; private set; } = 0;
    public int DaysSpentAfterProgression { get; private set; } = 0;

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
            AddDaysSpent(assignedTask.amountOfDays);
            assignedTaskIndex++;
            assignedTask = tasksInOrder[assignedTaskIndex];
            taskProgressionEvent.Raise();
        }
    }
    public void AddDaysSpent(int days)
    {
        CurrentAmountOfDaysAtWender = DaysSpentAfterProgression;
        DaysSpentAfterProgression += days; // Add the amount of days it takes to progress to the next task
    }
    public int GetCosts(int days)
    {
        return days * 100;
    }
    private void Reset()
    {
        if (resetTask)
        {
            assignedTaskIndex = 0;
            assignedTask = tasksInOrder[assignedTaskIndex];

            CurrentAmountOfDaysAtWender = 0;
            DaysSpentAfterProgression = 0;
        }
    }
}
