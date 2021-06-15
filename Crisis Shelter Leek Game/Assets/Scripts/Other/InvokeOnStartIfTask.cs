using UnityEngine;
using UnityEngine.Events;

public class InvokeOnStartIfTask : MonoBehaviour
{
    public UnityEvent thingsToDo;
    [SerializeField] private TaskJourney taskJourney;
    [SerializeField] private Task currentTask;

    private void Start()
    {
        if (currentTask == taskJourney.assignedTask)
        {
            thingsToDo.Invoke();
        }
    }

}
