using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Tooltip("The DialogueBoxVisualizer that should handle how to display the dialogue boxes.")]
    public DialogueBoxVisualizer dialogueBoxVisualizer;
    [SerializeField] private AudioSource voicePlayer;

    ConversationSection activeConversationSection;
    int indexOfcurrentlyShownDialogueBoxContent;

    public GameEvent DialogueHasStarted;
    public GameEvent DialogueHasEnded;
    public GameObjectGameEvent ConversationSectionHasStarted;
    public GameObjectGameEvent ConversationSectionHasEnded;

    public void StartConversationSection(ConversationSection conversationSection)
    {
        if (activeConversationSection == null)
            DialogueHasStarted.Raise();
        else
            ConversationSectionHasEnded.Raise(activeConversationSection.gameObject);

        ConversationSectionHasStarted.Raise(conversationSection.gameObject);

        activeConversationSection = conversationSection;
        indexOfcurrentlyShownDialogueBoxContent = 0;

        ShowDialogueBoxWithCurrentIndex();
    }

    private void ShowDialogueBoxWithCurrentIndex()
    {
        if (activeConversationSection.dialogueBoxContent[indexOfcurrentlyShownDialogueBoxContent].speakerVoice != null)
        voicePlayer.PlayOneShot(activeConversationSection.dialogueBoxContent[indexOfcurrentlyShownDialogueBoxContent].speakerVoice);
        // talk sound play
        if (CurrentIndexIsLastIndex() && ActiveConversationSectionHasChoicesAtEnd())
            dialogueBoxVisualizer.ShowDialogueBox(activeConversationSection.dialogueBoxContent[indexOfcurrentlyShownDialogueBoxContent], activeConversationSection.choicesAtEnd);
        else 
            dialogueBoxVisualizer.ShowDialogueBox(activeConversationSection.dialogueBoxContent[indexOfcurrentlyShownDialogueBoxContent]);
    }

    private bool CurrentIndexIsLastIndex() => (indexOfcurrentlyShownDialogueBoxContent + 1 == activeConversationSection.dialogueBoxContent.Length);

    private bool ActiveConversationSectionHasChoicesAtEnd() => (0 < activeConversationSection.choicesAtEnd.Length);

    public void OnContinueButtonPressed()
    {
        if (CurrentIndexIsLastIndex())
        {
            if (activeConversationSection.followUpConversationIfNoChoicesPresent != null)
            {
                StartConversationSection(activeConversationSection.followUpConversationIfNoChoicesPresent);
            }
            else
            {
                dialogueBoxVisualizer.HideDialogueBox();
                DialogueHasEnded.Raise();
                ConversationSectionHasEnded.Raise(activeConversationSection.gameObject);
                activeConversationSection = null;
            }
        }
        else
        {
            indexOfcurrentlyShownDialogueBoxContent++;
            ShowDialogueBoxWithCurrentIndex();
        }
    }

    public void OnDialogueChoiceHasBeenSelectedWithIndex(int indexOfChoice)
    {
        activeConversationSection.choicesAtEnd[indexOfChoice].Consequence.Invoke();

        ConversationSection followUpConversationForSelectedChoice = activeConversationSection.choicesAtEnd[indexOfChoice].followUpConversation;

        if (followUpConversationForSelectedChoice != null)
            StartConversationSection(followUpConversationForSelectedChoice);
        else
        {
            dialogueBoxVisualizer.HideDialogueBox();
            DialogueHasEnded.Raise();
            ConversationSectionHasEnded.Raise(activeConversationSection.gameObject);
        }
    }
}
