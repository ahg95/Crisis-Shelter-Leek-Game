using UnityEngine;

[RequireComponent(typeof(Interactable))]
[DisallowMultipleComponent]
public class DisableOnTask : MonoBehaviour
{
    [SerializeField] private TaskJourney taskJourney;
    [SerializeField] private Task[] tasks;

    private void Start()
    {
        DisableInteraction();
    }

    public void DisableInteraction()
    {
        bool currentTaskInList = false;

        for (int i = 0; i < tasks.Length; i++)
        {
            Task task = tasks[i];
            if (taskJourney.assignedTask == task)
            {
                currentTaskInList = true;
            }
        }

        if (currentTaskInList)
        {
            GetComponent<Interactable>().enabled = false;
            GetComponent<Outline>().enabled = false;
            gameObject.layer = 0;
        }
        else
        {
            GetComponent<Interactable>().enabled = true;
            GetComponent<Outline>().enabled = true;
            gameObject.layer = LayerMask.NameToLayer("Clickable");
        }
    }
}
