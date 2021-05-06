using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Task Journey", menuName = "Tasks/Task Journey")]
public class TaskJourney : ScriptableObject
{
    #region resetData
    public Task firstTask;
    #endregion

    [Space(15)]
    public Task assignedTask;

    [Space(20)]
    public Task[] tasksInOrder;

    private int assignedTaskInt = -1;

    public void Progress()
    {
        if (assignedTaskInt != tasksInOrder.Length - 1) // if not the last task
        {
            assignedTaskInt++;
            assignedTask = tasksInOrder[assignedTaskInt];

            //Debug.Log("Progressed Task!");
            //Debug.Log("Current Task: " + assignedTask);
        }
        else
        {
            //Debug.Log("Last Task Finished!");
        }
    }

    private void OnDisable()
    {
        // reset
        assignedTask = firstTask;
        assignedTaskInt = -1;
    }
}
