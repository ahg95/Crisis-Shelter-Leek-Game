using System.Collections;
using UnityEngine;

public class Talkable : Interactable
{
    [Space(20)]
    [SerializeField] private bool rotateToPlayer = false;
    [SerializeField] private TaskJourney taskJourney;
    [SerializeField] private DialogueManager dialogueManager;
    public ConversationTaskCombination[] conversationTaskCombination;

    public void StartConversationAccordingToCurrentPlayerTask()
    {
        StartCoroutine(RotateTowards());

        Task currentPlayerTask = taskJourney.assignedTask;
        
        ConversationSection conversationToStart = null;

        foreach (ConversationTaskCombination ctc in conversationTaskCombination)
        {
            if (ctc.task == currentPlayerTask)
            {
                conversationToStart = ctc.conversationSection;
                break;
            }
        }

        if (conversationToStart != null)
        {
            dialogueManager.StartConversationSection(conversationToStart);
        }
    }

    private IEnumerator RotateTowards()
    {
        Quaternion startingRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position);

        float rotationLength = Quaternion.Angle(transform.rotation, Camera.main.transform.rotation) / 360;

        float elapsedTime = 0;

        while (elapsedTime < rotationLength)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / rotationLength;

            transform.rotation = Quaternion.Slerp(startingRotation, targetRotation, progress);

            yield return new WaitForEndOfFrame();
        }
    }
}
