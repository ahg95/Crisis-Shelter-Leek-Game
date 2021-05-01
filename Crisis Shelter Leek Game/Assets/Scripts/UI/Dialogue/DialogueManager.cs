using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Tooltip("The DialogueBoxVisualizer that should handle how to display the dialogue boxes.")]
    public DialogueBoxVisualizer dialogueBoxVisualizer;

    Queue<DialogueBoxContent> dialogueBoxesToShow;
    DialogueBoxContent currentlyShownDialogueBox;

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

    public void ShowConversationSection(ConversationSection conversationSection)
    {
        DisableSystemsToDisableOnDialogue();

        EnqueueConversationSection(conversationSection);

        DequeueDialogueBoxAndShowIt();
    }

    private void EnqueueConversationSection(ConversationSection conversationSection)
    {
        dialogueBoxesToShow = ConvertConversationSectionIntoQueueOfDialogueBoxes(conversationSection);
    }

    private void DequeueDialogueBoxAndShowIt()
    {
        currentlyShownDialogueBox = dialogueBoxesToShow.Dequeue();
        dialogueBoxVisualizer.ShowDialogueBox(currentlyShownDialogueBox);
    }

    private Queue<DialogueBox> ConvertConversationSectionIntoQueueOfDialogueBoxes(ConversationSection conversationSection)
    {
        Queue<DialogueBox> dialogueBoxes = new Queue<DialogueBox>();

        DialogueBoxContent[] content = conversationSection.dialogueBoxContent;

        for (int i = 0; i < content.Length; i++)
        {
            DialogueBox dialogueBoxToAdd;

            if (i + 1 == content.Length)
                dialogueBoxToAdd = new DialogueBox(content[i].speaker.ToString(), content[i].content, conversationSection.choicesAtEnd);
            else
                dialogueBoxToAdd = new DialogueBox(content[i].speaker.ToString(), content[i].content);

            dialogueBoxes.Enqueue(dialogueBoxToAdd);
        }

        return dialogueBoxes;
    }

    public void ShowNextDialogueBoxOrHideIfNoneLeft()
    {
        if (ThereAreDialogueBoxesToShow())
        {
            DequeueDialogueBoxAndShowIt();
        } else
        {
            currentlyShownDialogueBox = null;
            dialogueBoxVisualizer.HideDialogueBox();
            EnableSystemsToDisableOnDialogue();
        }
    }

    private bool ThereAreDialogueBoxesToShow()
    {
        return (0 < dialogueBoxesToShow.Count);
    }

    public void OnDialogueChoiceHasBeenSelectedWithIndex(int indexOfChoice)
    {
        currentlyShownDialogueBox.choices[indexOfChoice].Consequence.Invoke();

        ConversationSection followUpConversation = currentlyShownDialogueBox.choices[indexOfChoice].followUpConversation;

        if (followUpConversation != null)
            EnqueueConversationSection(followUpConversation);

        ShowNextDialogueBoxOrHideIfNoneLeft();
    }

    public void DisableSystemsToDisableOnDialogue()
    {
        SetActivationOfSystemsToDisableOnDialogue(false);
    }

    public void EnableSystemsToDisableOnDialogue()
    {
        SetActivationOfSystemsToDisableOnDialogue(true);
    }

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
