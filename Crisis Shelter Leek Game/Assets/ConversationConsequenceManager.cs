using UnityEngine;

public class ConversationConsequenceManager : MonoBehaviour
{
    [SerializeField] private TaskJourney taskJourney;
    [SerializeField] ConversationSectionShouldTaskProgressAndUIUpdateBooleans[] settings;

    public void OnConversationSectionEnd(GameObject argument)
    {

    }
}

[System.Serializable]
public class ConversationSectionShouldTaskProgressAndUIUpdateBooleans
{
    public ConversationSection conversationSection;
    public bool shouldTaskProgress;
    public bool shouldUIUpdate;
}