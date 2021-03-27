using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal
{
    public Task task;

    public string title;
    public string description;
    public int currentAmount;
    public int requiredAmount;
    public bool isCompleted;

    //this function will be overriden everytime a task is being added;
    public virtual void Init()
    {

    }

    //checks if goal is completed
    public void Evaluate()
    {
        if(currentAmount >= requiredAmount)
        {
            isCompleted = true;
            task.CheckGoals();
        }
    }

}
