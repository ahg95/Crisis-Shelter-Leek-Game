using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueBox : MonoBehaviour
{
    public string speaker;
    [TextArea(1, 3)]
    public string dialogueText;
    public DialogueChoice[] choices;

    public UnityEvent OnDialogueContinued;

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