﻿using UnityEngine;

public class DialogueChooser : MonoBehaviour
{
    public DialogueSection dialogueIfNoTask;
    public DialogueAssociatedToTask[] taskWithAssociatedDialogues;
    private DialogueSection[] dialoguesInstances;

    private void Start()
    {
        dialoguesInstances = GameObject.FindGameObjectWithTag("Dialogues").GetComponentsInChildren<DialogueSection>();
    }

    public void PlayDialogueCorrespondingToTask()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CurrentTask taskList = player.GetComponent<CurrentTask>();
        GameObject dialogueManager = GameObject.Find("DialogueManager");

        if (taskList.assignedTask != null)
        {
            Task currentTask = taskList.assignedTask;

            // check the dialogueassociatedtotask array to see which dialogue is connected to the current task
            foreach (DialogueAssociatedToTask taskAssociatedToDialogue in taskWithAssociatedDialogues)
            {
                if (taskAssociatedToDialogue.task.taskID == currentTask.taskID)
                {
                    dialogueManager.GetComponent<DialogueManager>().ShowDialogueSection(taskAssociatedToDialogue.associatedDialogue);

                    //FindAndShowDialogueInstance(taskAssociatedToDialogue.associatedDialogue);

                    // gets it from the inspector == error. needs instance.
                    // dialogueManager.GetComponent<DialogueManager>().ShowDialogueSection(taskAssociatedToDialogue.associatedDialogue);
                }
            }
        }
        else
        {
            dialogueManager.GetComponent<DialogueManager>().ShowDialogueSection(dialogueIfNoTask);

            //FindAndShowDialogueInstance(dialogueIfNoTask);

            // show the dialogueIfNoTask dialoguesection
            // dialogueManager.GetComponent<DialogueManager>().ShowDialogueSection(dialogueIfNoTask);
        }

        /*void FindAndShowDialogueInstance(DialogueSection dialogueSectionAsset)
        {
            foreach (DialogueSection section in dialoguesInstances)
            {
                *//*
                // check if the dragged in prefab section is the same as the instance
                if (section.thisPrefab == dialogueSectionAsset)
                {
                    dialogueManager.GetComponent<DialogueManager>().ShowDialogueSection(section);
                }
                *//*


            }
        }*/
    }
}