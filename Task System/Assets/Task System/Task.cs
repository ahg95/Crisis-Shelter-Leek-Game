using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Task", menuName = "Tasks/New Task")]
public class Task : ScriptableObject
{
    public string title;
    public string description;
    public int taskReward;
    public int taskID;
    public bool taskCompleted;
    public List<Task> conditionTasks = new List<Task>();
}
