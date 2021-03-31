public class Goal
{
    public TaskAnastasia task;

    public string title;
    public string description;
    public int currentAmount;
    public int requiredAmount;
    public bool taskFinished;
    public bool isCompleted;

    //this function will be overriden everytime a task is being added;
    public virtual void Init()
    {
    }

    //checks if goal is completed
    public void Evaluate()
    {
        if(currentAmount >= requiredAmount || taskFinished)
        {
            isCompleted = true;
            task.CheckGoals();
        }
    }
}
