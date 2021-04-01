using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent onInteraction;

    public virtual void InteractWith()
    {
        onInteraction.Invoke();
        print("Interacting with interactable object!");
    }
}
