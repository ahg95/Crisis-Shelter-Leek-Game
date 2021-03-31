public class InteractPersonTest : Interactable
{
    public override void InteractWith()
    {
        // base.InteractWith();
        GetComponent<TaskGiver>().AssignTasks();
    }
}
