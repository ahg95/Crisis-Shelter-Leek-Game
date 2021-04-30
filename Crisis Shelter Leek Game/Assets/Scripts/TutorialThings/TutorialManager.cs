using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TutorialManager : MonoBehaviour
{

    public GameObject[] popUps; //the key tutorial objects
    public int popUpId;

    //popUp Animations
    public Animator[] rotationAnim;

    public float[] waitTime;

    //tutorial parts checks
    public bool[] tutorialParts;

    public bool tutorialActive = false;

    public bool isRotating = false;
    public bool isMoving = false;
    public bool isInteracting = false;

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

        //first tutorial is rotation
        if (popUpId == 0)
        {
            if (isRotating)
            {
                rotationAnim[0].SetBool("animOff", true);
                if (waitTime[0] <= 0)
                {
                    popUpId++;
                    dialogManager.DisplayNextSentence();
                }
                else
                {
                    waitTime[0] -= Time.deltaTime;
                }
            }
        }
        else if (popUpId == 1)//2nd is movement
        {
            if (isMoving)
            {
                rotationAnim[1].SetBool("animOff", true);

                if(waitTime[1] <= 0)
                {
                    popUpId++;
                    dialogManager.DisplayNextSentence();
                }
                else
                {
                    waitTime[1] -= Time.deltaTime;
                }
            }
        }
        else if (popUpId == 2)//3rd is interaction
        {
            if (isInteracting)
            {
                if (isMoving)
                {
                    rotationAnim[2].SetBool("animOff", true);

                    if (waitTime[2] <= 0)
                    {
                        popUpId++;
                        dialogManager.DisplayNextSentence();
                    }
                    else
                    {
                        waitTime[2] -= Time.deltaTime;
                    }
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
    /// <param name="partNr"> The order of the tutroial part that you wish to accomplish </param>
    public void CheckTutorialPart(int partNr)
    {
        if (tutorialActive)
        {
            tutorialParts[partNr] = true;
        }
    }



    public void CheckRotation (bool rotating)
    {
        if (tutorialActive)
        {
            isRotating = rotating;
        }
    }

    public void CheckMovement (bool moving)
    {
        if (tutorialActive)
        {
            isMoving = moving;
        }
    }

    public void CheckInteraction (bool interacting)
    {
        if (tutorialActive)
        {
            isInteracting = interacting;
        }
    }


}



//have arrows for moving to the left or right on the screen
//a text pop-up for moving to arrow => which is already highlighted
//an arrow pointing to an object you can interact with 
//a highlight over the task ui with a popup saying what's it for
