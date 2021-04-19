using UnityEngine;

[CreateAssetMenu(fileName = "Task Journey", menuName = "Tasks/Task Journey")]
public class TaskJourney : ScriptableObject
{
    #region resetData
    public Task firstTask;
    #endregion

    public Task assignedTask;
    [Space(20)]
    public Task[] tasksInOrder;

    private int assignedTaskInt = 0;

    private void OnEnable()
    {
        // reset
        assignedTask = firstTask;
        assignedTaskInt = 0;
    }
    public void Progress()
    {
        if (assignedTaskInt != tasksInOrder.Length - 1)
        {
            assignedTaskInt++;
            assignedTask = tasksInOrder[assignedTaskInt];
            Debug.Log("Progressed Task!");
            Debug.Log("Current Task: " + assignedTask);
        }
        else
        {
            Debug.Log("Last Task Finished!");
        }
    }
}
