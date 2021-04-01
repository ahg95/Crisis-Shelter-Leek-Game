using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public DialogueBoxVisualizer dialogueBoxVisualizer;

    public MonoBehaviour[] systemsToDisableWhenDialogueIsShown;

    Queue<DialogueBox> dialogueBoxesToShow;
    DialogueBox currentlyShownDialogueBox;

    GameObject[] gameObjectsToDisable;

    private void Start()
    {
        gameObjectsToDisable = GameObject.FindGameObjectsWithTag("disabledOnDialogue");
    }

    public void ShowDialogueSection(DialogueSection dialogueSection)
    {
        DisableOtherSystems();

        Debug.Log("Start dialogue");
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
            EnableOtherSystems();
        }
    }

    public void OnDialogueChoiceHasBeenSelectedWithIndex(int indexOfChoice)
    {
        Debug.Log("option has been selected");
        currentlyShownDialogueBox.choices[indexOfChoice].Consequence.Invoke();

        ShowNextDialogueBoxOrHideIfNoneLeft();
    }

    public void DisableOtherSystems()
    {
        SetSystemActivationState(false);
    }

    public void EnableOtherSystems()
    {
        SetSystemActivationState(true);
    }

    public void SetSystemActivationState(bool activated)
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
