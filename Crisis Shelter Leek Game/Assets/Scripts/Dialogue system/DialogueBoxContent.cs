using UnityEngine;

[System.Serializable]
public class DialogueBoxContent
{
    public enum Speaker
    {
        You,
        YouThinking,
        Karen,
        Gerard,
        Daisy,
        DescriptiveText,
        Stranger,
        frontDeskWorker
    };

    public Speaker speaker;

    [TextArea(1, 3)]
    public string content;
}
