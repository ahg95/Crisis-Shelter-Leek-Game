using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSection : MonoBehaviour
{
    [HideInInspector]
    public DialogueBox[] dialogueBoxes;

    private void Start()
    {
        dialogueBoxes = GetComponentsInChildren<DialogueBox>();
    }
}
