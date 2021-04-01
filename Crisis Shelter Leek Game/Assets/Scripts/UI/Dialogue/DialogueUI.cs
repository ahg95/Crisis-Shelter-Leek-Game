using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField]
    TextMeshProUGUI speakerTextBox;
    [SerializeField]
    TextMeshProUGUI dialogueTextBox;

    [Header("Options")]
    [SerializeField]
    GameObject[] choiceUIObjects;
    [SerializeField]
    TextMeshProUGUI[] choiceTextBoxes;

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
