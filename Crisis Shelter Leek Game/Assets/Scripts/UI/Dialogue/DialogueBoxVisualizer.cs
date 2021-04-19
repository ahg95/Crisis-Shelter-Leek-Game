using System.Collections;
using UnityEngine;

// <summary> This class is responsible for managing the logic behind showing a certain <c>DialogueBox</c>
// and connects the <c>DialogueManager</c> with the <c>DialogueUI</c> </summary>
public class DialogueBoxVisualizer : MonoBehaviour
{
    [Tooltip("The dialogueUI that should be used to visualize the dialogue boxes.")]
    public DialogueUI dialogueUI;

    [Tooltip("The animator responsible for moving the UI of the dialogue box up and down.")]
    public Animator animator;
    [Tooltip("The interval at which letters show.")]
    [Range(0, 0.1f)]
    [SerializeField] private float textShowSpeed = 0.05f;


    public virtual void ShowDialogueBox(DialogueBox dialogueBox)
    {
        dialogueUI.SetSpeaker(dialogueBox.speaker);

        StopAllCoroutines();
        StartCoroutine(SlowlyRevealDialogueText(dialogueBox.dialogueText));

        int nrOfChoices = dialogueBox.choices.Length;

        dialogueUI.OnlyShowSpecifiedNumberOfChoices(nrOfChoices);

        if (0 < nrOfChoices)
            dialogueUI.HideContinueButton();
        else
            dialogueUI.ShowContinueButton();

        for (int i = 0; i < nrOfChoices; i++)
        {
            dialogueUI.SetChoiceText(i, dialogueBox.choices[i].choiceText);
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

            yield return new WaitForSeconds(textShowSpeed);
        }
    }

    public virtual void HideDialogueBox()
    {
        animator.SetBool("show", false);
    }
}
