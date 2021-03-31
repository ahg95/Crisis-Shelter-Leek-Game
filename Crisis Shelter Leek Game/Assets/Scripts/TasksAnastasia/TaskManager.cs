using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public List<bool> assignedTasks;
    [SerializeField]
    private GameObject tasksObject;
    [SerializeField]
    private List<string> taskType;
    public List<TaskAnastasia> Task;


    void Start()
    {
        Task = new List<TaskAnastasia>(new TaskAnastasia[taskType.Count]);
        assignedTasks = new List<bool>(new bool[Task.Count]);
        tasksObject = GameObject.FindGameObjectWithTag("Tasks");
    }

    public void CheckCurrentTasks()
    {
        if (!TasksAssigned())
        {
            AssignTasks();
        }
        else
        {
            CheckTaskProgress();
        }
    }

    //Assign all tasks that are written in the "taskList"
    //Adds the tasks scripts to the "tasksObject"
    public void AssignTasks()
    {
        for (int i = 0; i < taskType.Count; i++)
        {
            assignedTasks[i] = true;

            List<System.Type> type = new List<System.Type>();
            type = new List<System.Type>(new System.Type[taskType.Count]);
            type[i] = System.Type.GetType(taskType[i]);
            Task[i] = (TaskAnastasia)tasksObject.AddComponent(type[i]);
        }
    }

    //When a task is completed add the days count to the player and destroy the task from the list of assigned tasks
    public void CheckTaskProgress()
    {
        for (int i = 0; i < Task.Count; i++)
        {
            if (Task[i].taskCompleted)
            {
                assignedTasks[i] = false;
                Task[i].AddDays();

                Destroy(tasksObject.GetComponent(taskType[i]));

            }
            else
            {
                Debug.Log(Task[i] + " is not complete");
            }
        }
    }

    //A boolean that checks if any tasks have been assigned
    private bool TasksAssigned()
    {
        bool tasksAreAssigned = false;

        // Loop through list of tasks assigned
        foreach (bool task in assignedTasks)
        {
            if (task == true) // If one or more tasks return true
            {
                tasksAreAssigned =  true; // return true
            }
        }
        return tasksAreAssigned;
    }
}