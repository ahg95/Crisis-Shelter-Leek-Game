using System.Collections;
using UnityEngine;

public class DialogueBoxVisualizer : MonoBehaviour
{
    public DialogueUI dialogueUI;

    public Animator animator;

    public void ShowDialogueBox(DialogueBox dialogueBox)
    {
        dialogueUI.SetSpeaker(dialogueBox.speaker);

        StopAllCoroutines();
        StartCoroutine(SlowlyRevealDialogueText(dialogueBox.dialogueText));

        int nrOfChoices = dialogueBox.choices.Length;

        if (0 < nrOfChoices)
            dialogueUI.HideContinueButton();
        else
            dialogueUI.ShowContinueButton();

        for (int i = 0; i < nrOfChoices; i++)
        {
            dialogueUI.SetOptionText(i, dialogueBox.choices[i].text);
        }

        animator.SetBool("show", true);
        animator.SetInteger("numberOfChoices", nrOfChoices);
    }

    IEnumerator SlowlyRevealDialogueText(string text)
    {
        string currentlyShownText = "";

        dialogueUI.SetDialogueText(currentlyShownText);

        foreach (char letter in text.ToCharArray())
        {
            currentlyShownText += letter;
            dialogueUI.SetDialogueText(currentlyShownText);

            yield return new WaitForSeconds(0.05f);
        }
    }

    public void HideDialogueBox()
    {
        animator.SetBool("show", false);
    }
}
