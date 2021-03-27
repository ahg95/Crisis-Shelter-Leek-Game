using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Task : MonoBehaviour
{
    public List<Goal> Goals = new List<Goal>();
    public string title;
    public string description;
    public bool taskCompleted;
    public int daysAmount;

    public Player player;

    private void Awake()
    {
        player = transform.parent.gameObject.GetComponent<Player>();
    }

    //Checks if all goals have been completed
    public void CheckGoals()
    {
        taskCompleted = Goals.All(g => g.isCompleted);
    }

    //Adds days after a task has been completed
    public void AddDays()
    {
        player.IncreaseDays(daysAmount);
    }
}
