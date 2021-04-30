using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TutorialManager1 : MonoBehaviour
{

    public GameObject[] popUps; //the key tutorial objects
    public int popUpId;

    public int n = 0;

    //popUp Animations
    public Animator[] rotationAnim;

    public float[] waitTime;

    //tutorial parts checks
    public bool[] tutorialParts;
    //prt1.Rotation
    //prt2.Movement
    //prt3.Interaction

    public bool tutorialActive = false;

    //tutorial dialogue
    public DialogManager dialogManager;
    // public DialogueTrigger dialogueTrigger;

    public GameObject skipTutorialButton;
    public GameObject finishDialogueButton;

    void Start()
    {
        skipTutorialButton = GameObject.Find("SkipTutorialButton");
        skipTutorialButton.SetActive(false);
        finishDialogueButton = GameObject.Find("FinishTutorialButton");
        finishDialogueButton.SetActive(false);

        foreach (GameObject popUp in popUps)
        {
            popUp.SetActive(false);
        }


        dialogManager = FindObjectOfType<DialogManager>();
        // dialogueTrigger = FindObjectOfType<DialogueTrigger>();
    }

    private void Update()
    {
        if (tutorialActive)
        {
            StartTutorial();
        }
    }

    public void StartTutorial()
    {
        tutorialActive = true;
        skipTutorialButton.SetActive(true);

        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpId) { popUps[i].SetActive(true); }
            else { popUps[i].SetActive(false); }
        }

        if (popUpId == n && n < popUps.Length){
            if (tutorialParts[n])
            {
                rotationAnim[n].SetBool("animOff", true);
                if (waitTime[n] <= 0)
                {
                    n++;
                    popUpId++;
                    dialogManager.DisplayNextSentence();
                }
                else
                {
                    waitTime[n] -= Time.deltaTime;
                }
            }
        }
        else if (popUpId == popUps.Length)
        {
            finishDialogueButton.SetActive(true);
            skipTutorialButton.SetActive(false);
        }
    }

    //this function will skip the tutorial 
    public void SkipTutorial()
    {
        popUpId = popUps.Length;
        dialogManager.EndDialogue();
        skipTutorialButton.SetActive(false);
    }

    /// <summary>
    /// This function will be called when the player succeded at accomplishing a tutorial part
    /// </summary>
    /// <param name="partNr"> The order of the tutorial part that you wish to accomplish from the array </param>
    public void CheckTutorialPart(int partNr)
    {
        if (tutorialActive)
        {
            tutorialParts[partNr] = true;
        }
    }
}



//have arrows for moving to the left or right on the screen
//a text pop-up for moving to arrow => which is already highlighted
//an arrow pointing to an object you can interact with 
//a highlight over the task ui with a popup saying what's it for
