using UnityEngine;

public class Talkable : Interactable
{
    [Space(20)]
    public ConversationTaskCombination[] conversationTaskCombination;
    public void ShowConversationSection(ConversationSection section)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        DialogueManager dialogueManager = player.GetComponentInChildren<DialogueManager>();

        dialogueManager.StartConversationSection(section);
    }
}
