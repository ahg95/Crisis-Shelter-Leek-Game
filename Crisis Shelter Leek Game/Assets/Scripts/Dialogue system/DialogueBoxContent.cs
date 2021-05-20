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
        Stranger
    };

    public Speaker speaker;
    public AudioClip speakerVoice;

    [TextArea(1, 3)]
    public string content;
}
