using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingDialogueUI : MonoBehaviour
{
    public DialogueBoxVisualizer dialogueVisualizer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            dialogueVisualizer.animator.SetBool("show", true);
            dialogueVisualizer.animator.SetInteger("numberOfChoices", 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            dialogueVisualizer.animator.SetBool("show", true);
            dialogueVisualizer.animator.SetInteger("numberOfChoices", 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            dialogueVisualizer.animator.SetBool("show", true);
            dialogueVisualizer.animator.SetInteger("numberOfChoices", 2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            dialogueVisualizer.animator.SetBool("show", true);
            dialogueVisualizer.animator.SetInteger("numberOfChoices", 3);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            dialogueVisualizer.animator.SetBool("show", false);
        }
    }
}
