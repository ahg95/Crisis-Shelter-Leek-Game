using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
     public List<bool> taskAssigned;
    [SerializeField]
    private GameObject tasksObject;
    [SerializeField]
    private List<string> taskType;
    public List<Task> Task;


    void Start()
    {
        Task = new List<Task>(new Task[taskType.Count]);
        taskAssigned = new List<bool>(new bool[Task.Count]);
    }

    public void Interact()
    {
        if (!TasksAssigned())
        {
            AssignTasks();
        }
        else
        {
            CheckForTask();
        }
    }

    //Assign all tasks that are written in the "taskList"
    //Adds the tasks scripts to the "tasksObject"
    public void AssignTasks()
    {
        for (int i = 0; i < taskType.Count; i++)
        {
            taskAssigned[i] = true;

            List<System.Type> type = new List<System.Type>();
            type = new List<System.Type>(new System.Type[taskType.Count]);
            type[i] = System.Type.GetType(taskType[i]);
            Task[i] = (Task)tasksObject.AddComponent(type[i]);
        }

    }

    //When a task is completed add the days count to the player and destroy the task from the list of assigned tasks
    public void CheckForTask()
    {
        for (int i = 0; i < Task.Count; i++)
        {
            if (Task[i].taskCompleted)
            {
                taskAssigned[i] = false;
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
        bool returnBool = false;

        // Loop through list of tasks assigned
        foreach (bool boolean in taskAssigned)
        {
            if (boolean == true) // If one or more tasks return true
            {
                returnBool =  true; // return true
            }
        }
        return returnBool;
    }
}