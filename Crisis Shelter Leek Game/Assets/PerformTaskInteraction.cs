using UnityEngine;

public class PerformTaskInteraction : Interactable
{
    [SerializeField] private Task taskToPerform;

    public override void InteractWith()
    {
        // base.InteractWith();
        PerformTask.TryPerformTask(taskToPerform);
    }
}
