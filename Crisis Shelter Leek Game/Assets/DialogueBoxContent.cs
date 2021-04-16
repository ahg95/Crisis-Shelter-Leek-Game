[System.Serializable]
public class DialogueBoxContent
{
    public enum Speaker
    {
        Karen,
        Gerard,
        Daisy
    };
    public Speaker speaker;

    [UnityEngine.TextArea(1, 2)]
    public string content;
}
