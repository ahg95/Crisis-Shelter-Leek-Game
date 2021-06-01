using UnityEngine;

public class ConversationConsequenceManager : MonoBehaviour
{
    [SerializeField] private TaskJourney taskJourney;
    [SerializeField] private UISystem uiSystem;
    [SerializeField] ConversationConsequenceSettings[] settings;

    public void OnConversationSectionEnd(GameObject argument) // argument = the task which progressed
    {
        // print(argument.name);
        for (int i = 0; i < settings.Length; i++)
        {
            ConversationConsequenceSettings conversationConsequence = settings[i];
            if (conversationConsequence.conversationSection == argument.GetComponent<ConversationSection>())
            {
                if (conversationConsequence.shouldTaskProgress)
                {
                    taskJourney.Progress();
                }
                if (conversationConsequence.shouldUIUpdate)
                {
                    uiSystem.updateTaskUI();
                }
            }
        }
    }
}

[System.Serializable]
public class ConversationConsequenceSettings
{
    public ConversationSection conversationSection;
    public bool shouldTaskProgress;
    public bool shouldUIUpdate;
}