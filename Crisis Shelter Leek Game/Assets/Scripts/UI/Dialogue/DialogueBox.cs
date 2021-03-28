using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueBox : MonoBehaviour
{
    public string speaker;
    public string dialogueText;
    public DialogueChoice[] choices;

    /*
    public DialogueBox(string speaker, string dialogueText)
    {
        this.speaker = speaker;
        this.dialogueText = dialogueText;
        choices = new DialogueChoice[0];
    }

    public DialogueBox(string speaker, string dialogueText, DialogueChoice[] choices)
    {
        this.speaker = speaker;
        this.dialogueText = dialogueText;
        this.choices = choices;
    }
    */
}