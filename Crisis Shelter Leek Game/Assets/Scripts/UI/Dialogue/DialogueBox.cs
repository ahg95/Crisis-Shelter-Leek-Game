using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueBox
{
    public string speaker;
    public string dialogueText;
    public Choice[] choices;

    public DialogueBox(string speaker, string dialogueText)
    {
        this.speaker = speaker;
        this.dialogueText = dialogueText;
        choices = new Choice[0];
    }

    public DialogueBox(string speaker, string dialogueText, Choice[] choices)
    {
        this.speaker = speaker;
        this.dialogueText = dialogueText;
        this.choices = choices;
    }
}