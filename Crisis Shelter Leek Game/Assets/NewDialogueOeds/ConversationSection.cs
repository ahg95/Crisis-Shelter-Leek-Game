using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation Section", menuName = "Tasks/New Conversation Section")]
public class ConversationSection : ScriptableObject
{
    [HideInInspector] public TaskJourney taskJourney;

    public DialogueBoxContent[] dialogues;

    public void Progress()
    {
        Debug.Log("Progress!");
        taskJourney.Progress();
    }
}
