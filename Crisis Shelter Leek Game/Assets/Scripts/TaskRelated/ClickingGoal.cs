using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickingGoal : Goal
{
    public string clickId;
    public ClickingGoal(Task task, string clickID, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.task = task;
        this.clickId = clickID;
        this.description = description;
        this.isCompleted = completed;
        this.currentAmount = currentAmount;
        this.requiredAmount = requiredAmount;
    }

    
    public override void Init()
    {
        base.Init();
        Events.current.onButtonClicked += ClickingDone;
    }

    //adds to the currentAmount everytime a specific button is being clicked and evaluates if the goal is completed
    public void ClickingDone(string id)
    {
        if (id == this.clickId )
        {
            Debug.Log(this.clickId);
            this.currentAmount++;

            Evaluate();
        }
    }
}
