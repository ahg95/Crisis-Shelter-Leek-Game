using TMPro;
using UnityEngine;

// <summary> This class is just there to mask which UI elements are actually used in the scene. </summary>
public class DialogueUI : MonoBehaviour
{
    [Header("Dialogue")]
    [Tooltip("The text element in which the speaker of the dialogue should be shown.")]
    [SerializeField]
    TextMeshProUGUI speakerTextBox;
    [Tooltip("The text element in which the spoken or thought dialogue should be shown.")]
    [SerializeField]
    TextMeshProUGUI dialogueTextBox;

    [Header("Options")]
    [Tooltip("The button gameObjects that are used as choices, in order.")]
    [SerializeField]
    GameObject[] choiceUIObjects;
    [Tooltip("The text elements that are used in the choices, in order.")]
    [SerializeField]
    TextMeshProUGUI[] choiceTextBoxes;

    [Header("Buttons")]
    [Tooltip("The button gameObject with which the dialogue should continue.")]
    [SerializeField]
    GameObject continueButton;

    public void SetSpeaker(string name)
    {
        speakerTextBox.text = name;
    }

    public void SetSpeakerNameColor(Color speakerNameColor)
    {
        speakerTextBox.color = speakerNameColor;
    }

    public void SetDialogueText(string dialogueText)
    {
        dialogueTextBox.text = dialogueText;
    }

    public void SetDialogueTextToItalicFont()
    {
        dialogueTextBox.fontStyle = FontStyles.Italic;
    }

    public void SetDialogueTextToRegularFont()
    {
        dialogueTextBox.fontStyle = FontStyles.Normal;
    }

    public void SetChoiceText(int optionIndex, string text)
    {
        if (optionIndex < 0 || choiceTextBoxes.Length <= optionIndex)
        {
            Debug.LogError("optionIndex is out of bounds.");
        } else
        {
            choiceTextBoxes[optionIndex].text = text;
        }
    }

    public void OnlyShowSpecifiedNumberOfChoices(int numberOfChoices)
    {
        for (int i = 0; i < choiceUIObjects.Length; i++)
        {
            if (i < numberOfChoices)
                choiceUIObjects[i].SetActive(true);
            else
                choiceUIObjects[i].SetActive(false);
        }
    }

    public void HideContinueButton()
    {
        continueButton.SetActive(false);
    }

    public void ShowContinueButton()
    {
        continueButton.SetActive(true);
    }
}
