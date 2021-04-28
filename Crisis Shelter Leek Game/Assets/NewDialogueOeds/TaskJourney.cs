using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Task Journey", menuName = "Tasks/Task Journey")]
public class TaskJourney : ScriptableObject
{
    #region resetData
    public Task firstTask;
    #endregion

    private GameObject taskTextGameObject;
    private GameObject locationTextGameObject;

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

            UpdateUI();

            //Debug.Log("Progressed Task!");
            //Debug.Log("Current Task: " + assignedTask);
        }
        else
        {
            //Debug.Log("Last Task Finished!");
        }
    }
    private void UpdateUI()
    {
        taskTextGameObject.GetComponent<TextMeshProUGUI>().SetText(assignedTask.description);
        locationTextGameObject.GetComponent<TextMeshProUGUI>().SetText(assignedTask.location.ToString());
    }
    private void OnEnable()
    {
        taskTextGameObject = GameObject.Find("CURRENTTASK");
        locationTextGameObject = GameObject.Find("TASKLOCATION");
    }
    private void OnDisable()
    {
        // reset
        assignedTask = firstTask;
        assignedTaskInt = -1;
    }
}
