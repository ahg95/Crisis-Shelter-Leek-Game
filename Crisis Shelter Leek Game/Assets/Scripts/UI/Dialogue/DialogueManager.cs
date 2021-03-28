using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public DialogueBoxVisualizer dialogueBoxVisualizer;

    Queue<DialogueBox> dialogueBoxesToShow;
    DialogueBox currentlyShownDialogueBox;

    public void ShowDialogueSection(DialogueSection dialogueSection)
    {
        dialogueBoxesToShow = new Queue<DialogueBox>(dialogueSection.dialogueBoxes);
        ShowNextDialogueBoxOrHideIfNoneLeft();
    }

    public void ShowNextDialogueBoxOrHideIfNoneLeft()
    {
        if (0 < dialogueBoxesToShow.Count)
        {
            currentlyShownDialogueBox = dialogueBoxesToShow.Dequeue();
            dialogueBoxVisualizer.ShowDialogueBox(currentlyShownDialogueBox);
        } else
        {
            currentlyShownDialogueBox = null;
            dialogueBoxVisualizer.HideDialogueBox();
        }
    }

    public void OnDialogueChoiceHasBeenSelectedWithIndex(int indexOfChoice)
    {
        Debug.Log("option has been selected");
        currentlyShownDialogueBox.choices[indexOfChoice].Consequence.Invoke();

        ShowNextDialogueBoxOrHideIfNoneLeft();
    }
}
