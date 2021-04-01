using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Tooltip("The DialogueBoxVisualizer that should handle how to display the dialogue boxes.")]
    public DialogueBoxVisualizer dialogueBoxVisualizer;

    Queue<DialogueBox> dialogueBoxesToShow;
    DialogueBox currentlyShownDialogueBox;

    // Contains all gameObjects that should be disabled when there is dialogue.
    GameObject[] gameObjectsToDisable;

    private void Start()
    {
        gameObjectsToDisable = GameObject.FindGameObjectsWithTag("disabledOnDialogue");
    }

    public void ShowDialogueSection(DialogueSection dialogueSection)
    {
        DisableSystemsToDisableOnDialogue();

        dialogueBoxesToShow = new Queue<DialogueBox>(dialogueSection.dialogueBoxes);
        ShowNextDialogueBoxOrHideIfNoneLeft();
    }

    public void ShowNextDialogueBoxOrHideIfNoneLeft()
    {
        if (0 < dialogueBoxesToShow.Count)
        {
            currentlyShownDialogueBox = dialogueBoxesToShow.Dequeue();
            dialogueBoxVisualizer.ShowDialogueBox(currentlyShownDialogueBox);
            currentlyShownDialogueBox.OnDialogueContinued.Invoke();
        } else
        {
            currentlyShownDialogueBox = null;
            dialogueBoxVisualizer.HideDialogueBox();
            EnableSystemsToDisableOnDialogue();
        }
    }

    public void OnDialogueChoiceHasBeenSelectedWithIndex(int indexOfChoice)
    {
        currentlyShownDialogueBox.choices[indexOfChoice].Consequence.Invoke();

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
