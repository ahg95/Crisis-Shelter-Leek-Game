using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Tooltip("The DialogueBoxVisualizer that should handle how to display the dialogue boxes.")]
    public DialogueBoxVisualizer dialogueBoxVisualizer;

    ConversationSection activeConversationSection;
    int indexOfcurrentlyShownDialogueBoxContent;

    // Contains all gameObjects that should be disabled when there is dialogue.
    GameObject[] gameObjectsToDisable;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        gameObjectsToDisable = GameObject.FindGameObjectsWithTag("disabledOnDialogue");
    }

    public void StartConversationSection(ConversationSection conversationSection)
    {
        DisableSystemsToDisableOnDialogue();

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
            dialogueBoxVisualizer.HideDialogueBox();
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
            dialogueBoxVisualizer.HideDialogueBox();
    }

    public void DisableSystemsToDisableOnDialogue() => SetActivationOfSystemsToDisableOnDialogue(false);

    public void EnableSystemsToDisableOnDialogue() => SetActivationOfSystemsToDisableOnDialogue(true);

    public void SetActivationOfSystemsToDisableOnDialogue(bool activated)
    {
        InteractWith interactionScript = FindObjectOfType<InteractWith>();

        foreach (GameObject objectToDisable in gameObjectsToDisable)
        {
            objectToDisable.SetActive(activated);
        }

        if (interactionScript != null)
            interactionScript.enabled = activated;
        else
            Debug.LogWarning("DialogueManager couldn't find interactionScript to disable. Did you forget to put it into the scene?");
    }
}
