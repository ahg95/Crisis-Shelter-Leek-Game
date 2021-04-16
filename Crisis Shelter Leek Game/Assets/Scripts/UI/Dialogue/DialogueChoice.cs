using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Dialogue Choice", menuName = "Tasks/New Dialogue Choice")]
public class DialogueChoice : ScriptableObject
{
    [HideInInspector]
    public string text; // DELETELATER

    [SerializeField] private TaskJourney taskJourney;

    [TextArea(1, 2)]
    public string choiceText;
    [Space(10)]
    public UnityEvent Consequence;


    private void ApplyConsequences()
    {
        // GameObject.FindGameObjectWithTag("DCCManager").ApplyConsequenceOfChoice(this);
    }
    public void ProgressInTask()
    {
        taskJourney.Progress();
    }
}
