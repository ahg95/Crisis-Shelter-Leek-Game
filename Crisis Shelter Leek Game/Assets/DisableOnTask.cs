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
            GetComponent<Interactable>().enabled = false;
            GetComponent<Outline>().enabled = false;
            GetComponent<Collider>().enabled = false;
            gameObject.layer = 0;
        }
    }
}
