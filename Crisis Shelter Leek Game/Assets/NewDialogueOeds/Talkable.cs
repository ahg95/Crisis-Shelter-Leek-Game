using UnityEngine;

public class Talkable : Interactable
{
    private TaskJourney journey;
    [Space(20)]
    public ConversationTaskCombination[] conversationTaskCombinations;

    public override void Start()
    {
        base.Start();
        journey = ScriptableObject.CreateInstance<TaskJourney>();

        // start converstationTaskCombination based on journey.assignedtask
    }
}
