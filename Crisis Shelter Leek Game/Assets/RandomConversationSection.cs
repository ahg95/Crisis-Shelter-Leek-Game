using UnityEngine;

public class RandomConversationSection : MonoBehaviour
{
    [SerializeField] private DialogueManager manager;
    [SerializeField] private ConversationSection[] sections;
    private static int conversationSectionToShow = 0;

    public void PickConversationSection()
    {
        manager.StartConversationSection(sections[conversationSectionToShow]);

        if (conversationSectionToShow == 2)
        {
            conversationSectionToShow = 0;
        }
        else
        {
            conversationSectionToShow++;
        }
    }
}
