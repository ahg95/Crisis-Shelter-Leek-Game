using UnityEngine;

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

    [TextArea(1, 2)]
    public string content;

    public Speaker speaker;
}
