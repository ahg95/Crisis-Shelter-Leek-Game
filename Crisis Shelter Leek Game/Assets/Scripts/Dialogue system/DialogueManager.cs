using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Tooltip("The DialogueBoxVisualizer that should handle how to display the dialogue boxes.")]
    public DialogueBoxVisualizer dialogueBoxVisualizer;

    ConversationSection activeConversationSection;
    int indexOfcurrentlyShownDialogueBoxContent;

    public GameObjectGameEvent DialogueStarted;
    public GameObjectGameEvent DialogueEnded;

    public void StartConversationSection(ConversationSection conversationSection)
    {
        if (activeConversationSection == null)
            DialogueStarted.Raise(conversationSection.gameObject);

        activeConversationSection = conversationSection;
        indexOfcurrentlyShownDialogueBoxContent = 0;

        ShowDialogueBoxWithCurrentIndex();
    }

    private void ShowDialogueBoxWithCurrentIndex()
    {
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
                DialogueEnded.Raise(activeConversationSection.gameObject);
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
            DialogueEnded.Raise(activeConversationSection.gameObject);
        }
    }
}
