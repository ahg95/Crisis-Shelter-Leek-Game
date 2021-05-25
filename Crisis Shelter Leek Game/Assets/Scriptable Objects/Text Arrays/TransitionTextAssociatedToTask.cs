using UnityEngine;

[CreateAssetMenu(fileName = "Text Array", menuName = "Text Array")]
public class TransitionTextAssociatedToTask : ScriptableObject
{
    public Task associatedTask;
    [TextArea(0, 1)]
    public string[] textArray;
}
