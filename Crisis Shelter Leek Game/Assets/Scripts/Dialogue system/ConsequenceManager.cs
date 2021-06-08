using UnityEngine;
using UnityEngine.Events;

public class ConsequenceManager : MonoBehaviour
{
    [SerializeField] private TaskJourney taskJourney;
    [SerializeField] private UISystem uiSystem;
    [SerializeField] ConversationConsequenceSettings[] settings;

    public void OnConversationSectionEnd(GameObject argument) // argument = the task which progressed
    {
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

                conversationConsequence.consequenceEvent.Invoke();
            }
        }
    }

    private void OnValidate() // Update the element names of the array
    {
        if (!Application.isPlaying)
        {
            foreach (ConversationConsequenceSettings setting in settings)
            {
                setting.elementName = setting.conversationSection.name;
            }
        }
    }
}

[System.Serializable]
public class ConversationConsequenceSettings
{
    [HideInInspector] public string elementName;
    public ConversationSection conversationSection;
    public bool shouldTaskProgress;
    public bool shouldUIUpdate;
    public UnityEvent consequenceEvent;
}