using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkable : Interactable
{
    private TaskJourney journey;
    public ConversationTaskCombination convTaskCombi;

    public override void Start()
    {
        base.Start();
        journey = ScriptableObject.CreateInstance<TaskJourney>();
    }
}
