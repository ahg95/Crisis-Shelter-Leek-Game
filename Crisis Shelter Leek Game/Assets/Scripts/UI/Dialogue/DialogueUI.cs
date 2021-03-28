using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DialogueUI : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField]
    TextMeshProUGUI speakerTextBox;
    [SerializeField]
    TextMeshProUGUI dialogueTextBox;

    [Header("Options")]
    [SerializeField]
    TextMeshProUGUI[] optionTextBoxes;

    [Header("Buttons")]
    [SerializeField]
    GameObject continueButton;

    public void SetSpeaker(string name)
    {
        speakerTextBox.text = name;
    }

    public void SetDialogueText(string dialogueText)
    {
        dialogueTextBox.text = dialogueText;
    }

    public void SetOptionText(int optionIndex, string text)
    {
        if (optionIndex < 0 || optionTextBoxes.Length <= optionIndex)
        {
            Debug.LogError("optionIndex is out of bounds.");
        } else
        {
            optionTextBoxes[optionIndex].text = text;
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
