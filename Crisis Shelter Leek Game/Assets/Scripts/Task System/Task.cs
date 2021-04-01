using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Task", menuName = "Tasks/New Task")]
public class Task : ScriptableObject
{
    public string title;
    public string description;
    public int amountOfDays;
    public locations location;
    public enum locations
    {
        Municipality,
        HousingCorporation,
        Zienn
    };
    public int taskID;
    public bool taskCompleted;
    public List<Task> conditionTasks = new List<Task>();

    public void SetAsCompleted()
    {
        taskCompleted = true;
    }
}
