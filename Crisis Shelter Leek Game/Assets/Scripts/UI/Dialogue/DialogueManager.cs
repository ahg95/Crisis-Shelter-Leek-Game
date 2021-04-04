using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Tooltip("The DialogueBoxVisualizer that should handle how to display the dialogue boxes.")]
    public DialogueBoxVisualizer dialogueBoxVisualizer;

    Queue<DialogueBox> dialogueBoxesToShow;
    DialogueBox currentlyShownDialogueBox;

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

    public void ShowDialogueSection(DialogueSection dialogueSection)
    {
        DisableSystemsToDisableOnDialogue();

        dialogueBoxesToShow = new Queue<DialogueBox>(dialogueSection.dialogueBoxes);

        if (currentlyShownDialogueBox == null)
            ShowNextDialogueBoxOrHideIfNoneLeft();
    }

    public void ShowNextDialogueBoxOrHideIfNoneLeft()
    {
        if (currentlyShownDialogueBox != null)
            currentlyShownDialogueBox.OnDialogueContinued.Invoke();

        if (0 < dialogueBoxesToShow.Count)
        {
            currentlyShownDialogueBox = dialogueBoxesToShow.Dequeue();
            dialogueBoxVisualizer.ShowDialogueBox(currentlyShownDialogueBox);
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
