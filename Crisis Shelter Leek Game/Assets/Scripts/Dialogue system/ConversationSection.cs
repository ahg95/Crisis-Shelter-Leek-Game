using UnityEngine;
public class ConversationSection : MonoBehaviour
{
    public DialogueBoxContent[] dialogueBoxContent;

    [Space(10)]
    public Choice[] choicesAtEnd;

    [Space(10)]
    public ConversationSection followUpConversationIfNoChoicesPresent;
}