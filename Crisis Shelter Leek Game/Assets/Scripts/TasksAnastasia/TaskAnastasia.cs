using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskAnastasia : MonoBehaviour
{
    public List<Goal> Goals = new List<Goal>();
    public string title;
    public string description;
    public bool taskCompleted;
    public int daysAmount;

    //Checks if all goals have been completed
    public void CheckGoals()
    {
        taskCompleted = Goals.All(goal => goal.isCompleted);
    }

    //Adds days after a task has been completed
    public void AddDays()
    {
        GlobalStats.IncreaseStats(daysAmount, daysAmount * 100);
    }
}
