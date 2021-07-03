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

    [Tooltip("The colour of the name of the speaker when the player talks.")]
    [SerializeField]
    private Color SpeakerNameColourYou;

    [Tooltip("The colour of the name of the speaker when the player is thinking to themselves.")]
    [SerializeField]
    private Color SpeakerNameColourYouThinking;

    [Tooltip("The colour of the name of the speaker when Karen talks.")]
    [SerializeField]
    private Color SpeakerNameColourKaren;

    [Tooltip("The colour of the name of the speaker when Gerard talks.")]
    [SerializeField]
    private Color SpeakerNameColourGerard;

    [Tooltip("The colour of the name of the speaker when Daisy talks.")]
    [SerializeField]
    private Color SpeakerNameColourDaisy;

    [Tooltip("The colour of the name of the speaker when a stranger talks.")]
    [SerializeField]
    private Color SpeakerNameColourStranger;
    [Tooltip("The colour of Daisy before you know her name.")]
    [SerializeField]
    private Color SpeakerNameColourFrontDesk = Color.yellow;

    public virtual void ShowDialogueBox(DialogueBoxContent dialogueBoxContent, Choice[] choices = null)
    {
        dialogueUI.SetSpeaker(GetNameToDisplayForSpeaker(dialogueBoxContent.speaker));
        dialogueUI.SetSpeakerNameColor(GetSpeakerNameColorForSpeaker(dialogueBoxContent.speaker));

        if (dialogueBoxContent.speaker == DialogueBoxContent.Speaker.BeschrijvendeTekst ||
            dialogueBoxContent.speaker == DialogueBoxContent.Speaker.Denkende)
            dialogueUI.SetDialogueTextToItalicFont();
        else
            dialogueUI.SetDialogueTextToRegularFont();

        StopAllCoroutines();
        StartCoroutine(SlowlyRevealDialogueText(dialogueBoxContent.content));

        if (choices != null)
        {
            int nrOfChoices = choices.Length;

            dialogueUI.OnlyShowSpecifiedNumberOfChoices(nrOfChoices);
            dialogueUI.HideContinueButton();

            for (int i = 0; i < nrOfChoices; i++)
            {
                dialogueUI.SetChoiceText(i, choices[i].choiceText);
            }

            animator.SetInteger("numberOfChoices", nrOfChoices);
        } else
        {
            dialogueUI.OnlyShowSpecifiedNumberOfChoices(0);
            dialogueUI.ShowContinueButton();
            animator.SetInteger("numberOfChoices", 0);
        }

        animator.SetBool("show", true);
    }

    private string GetNameToDisplayForSpeaker(DialogueBoxContent.Speaker speaker)
    {
        string textDoDisplay;

        switch(speaker)
        {
            case DialogueBoxContent.Speaker.BeschrijvendeTekst:
                textDoDisplay = "";
                break;
            case DialogueBoxContent.Speaker.Denkende:
                textDoDisplay = "Denkende";
                break;
            case DialogueBoxContent.Speaker.receptieMedewerker:
                textDoDisplay = "Receptie Medewerker";
                break;
            default:
                textDoDisplay = speaker.ToString();
                break;
        }

        return textDoDisplay;
    }

    private Color GetSpeakerNameColorForSpeaker(DialogueBoxContent.Speaker speaker)
    {
        Color colorOfSpeakerName;

        switch (speaker)
        {
            case DialogueBoxContent.Speaker.Jij:
                colorOfSpeakerName = SpeakerNameColourYou;
                break;
            case DialogueBoxContent.Speaker.Denkende:
                colorOfSpeakerName = SpeakerNameColourYouThinking;
                break;
            case DialogueBoxContent.Speaker.Truus:
                colorOfSpeakerName = SpeakerNameColourKaren;
                break;
            case DialogueBoxContent.Speaker.Gerard:
                colorOfSpeakerName = SpeakerNameColourGerard;
                break;
            case DialogueBoxContent.Speaker.Roos:
                colorOfSpeakerName = SpeakerNameColourDaisy;
                break;
            case DialogueBoxContent.Speaker.Vreemde:
                colorOfSpeakerName = SpeakerNameColourStranger;
                break;
            case DialogueBoxContent.Speaker.receptieMedewerker:
                colorOfSpeakerName = SpeakerNameColourFrontDesk;
                break;
            default:
                colorOfSpeakerName = Color.white;
                break;
        }

        return colorOfSpeakerName;
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
