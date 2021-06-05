using UnityEngine;
using UnityEngine.Events;

public class CoachInBedroomEvent : MonoBehaviour
{
    public UnityEvent sceneLoaded;
    [SerializeField] private TaskJourney taskJourney;
    [SerializeField] private Task currentTask;

    private void Start()
    {
        if (currentTask == taskJourney.assignedTask)
        {
            sceneLoaded.Invoke();
        }
    }

}
