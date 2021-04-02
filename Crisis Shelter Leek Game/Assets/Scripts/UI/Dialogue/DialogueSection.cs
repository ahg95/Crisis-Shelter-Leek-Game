using UnityEngine;

public class DialogueSection : MonoBehaviour
{
    [HideInInspector]
    public DialogueBox[] dialogueBoxes;
    public DialogueSection thisPrefab;
    private void OnEnable()
    {
        dialogueBoxes = GetComponentsInChildren<DialogueBox>();
    }
}
