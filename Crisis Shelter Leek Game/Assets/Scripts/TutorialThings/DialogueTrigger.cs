using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        GameObject.FindObjectOfType<TutorialDialogueManager>().StartDialogue(dialogue);
    }
}
