using UnityEngine;

[System.Serializable]
public class DialogueBoxContent
{
    public enum Speaker
    {
        Jij,
        Denkende,
        Truus,
        Gerard,
        Roos,
        BeschrijvendeTekst,
        Vreemde,
        receptieMedewerker
    };

    public Speaker speaker;

    [TextArea(1, 3)]
    public string content;
}
