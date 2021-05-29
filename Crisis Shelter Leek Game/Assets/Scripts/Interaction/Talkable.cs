using UnityEngine;

public class Talkable : Interactable
{
    [Space(20)]
    [SerializeField] private TaskJourney taskJourney;
    [SerializeField] private DialogueManager dialogueManager;
    public ConversationTaskCombination[] conversationTaskCombination;

    public void StartConversationAccordingToCurrentPlayerTask()
    {
        Task currentPlayerTask = taskJourney.assignedTask;
        
        ConversationSection conversationToStart = null;

        foreach (ConversationTaskCombination ctc in conversationTaskCombination)
        {
            if (ctc.task == currentPlayerTask)
            {
                conversationToStart = ctc.conversationSection;
                break;
            }
        }

        if (conversationToStart != null)
            dialogueManager.StartConversationSection(conversationToStart);
    }
}
