using UnityEngine;

public class Talkable : Interactable
{
    [Space(20)]
    [SerializeField] private DialogueManager dialogueManager;
    public ConversationTaskCombination[] conversationTaskCombination;
    
    public override void OnValidate()
    {
        base.OnValidate();

        if (dialogueManager == null)
        {
            dialogueManager = FindObjectOfType<DialogueManager>();
            if (dialogueManager == null)
            {
                print("No dialogue manager in the scene");
            }
        }
    }
    public void ShowConversationSection(ConversationSection section)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        DialogueManager dialogueManager = player.GetComponentInChildren<DialogueManager>();

        dialogueManager.StartConversationSection(section);
    }
}
