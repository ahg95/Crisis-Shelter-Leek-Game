using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueBoxContent
{
    public enum Speaker
    {
        You,
        Karen,
        Gerard,
        Daisy
    };
    public Speaker speaker;

    [TextArea(1, 2)]
    public string content;

    [Space(20)]
    public ChoiceTest[] choices;
}
