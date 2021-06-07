using UnityEngine;

[RequireComponent(typeof(Interactable))]
[DisallowMultipleComponent]
public class DisableOnTask : MonoBehaviour
{
    [SerializeField] private TaskJourney taskJourney;
    [SerializeField] private Task[] tasks;
    [Header("Settings")]
    [SerializeField] private bool disableInteraction = false;

    private void Start()
    {
        if (disableInteraction)
        {
            for (int i = 0; i < tasks.Length; i++)
            {
                Task task = tasks[i];
                if (taskJourney.assignedTask == task)
                {
                    GetComponent<Interactable>().enabled = false;
                    GetComponent<Outline>().enabled = false;
                    GetComponent<Collider>().enabled = false;
                    gameObject.layer = 0;
                }
            }
        }
    }
}
