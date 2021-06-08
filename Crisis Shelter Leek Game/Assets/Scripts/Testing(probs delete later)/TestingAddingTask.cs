using UnityEngine;

public class TestingAddingTask : MonoBehaviour
{
    [SerializeField] private TaskJourney taskJourney;

    public void ProgressTask()
    {
        taskJourney.Progress();
    }    
    public void UnProgressTask()
    {
        taskJourney.assignedTaskIndex--;
        taskJourney.assignedTask = taskJourney.tasksInOrder[taskJourney.assignedTaskIndex];
    }
}
