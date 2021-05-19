using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TutorialManager : MonoBehaviour
{

    [SerializeField]private GameObject[] popUps; //the key tutorial objects
    [SerializeField] private Animator[] rotationAnim; //popUp Animations
    [SerializeField] private float[] waitTime; //how much you have to wait until you disable the object again(disable after the end animation has finished)

    [SerializeField] private int popUpId; // the id for every tutorial part
     private int n = 0;

    //tutorial parts checks
    [SerializeField] private bool[] tutorialParts;

    private bool tutorialActive = false;

    //tutorial dialogue
    private DialogManager dialogManager;
    private GameObject skipTutorialButton;
    private GameObject finishDialogueButton;

    //tutorial components
    private OnButtonHover[] cameraRot;
    private Navigation nav;
    private Interactable[] camZoom;

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

        dialogManager = gameObject.GetComponentInChildren<DialogManager>();
        cameraRot = GameObject.FindWithTag("Player").GetComponentsInChildren<OnButtonHover>();
        nav = GameObject.FindWithTag("Player").GetComponent<Navigation>();
        camZoom = GameObject.FindObjectsOfType<Interactable>();
        nav.enabled = false;
    }

    private void Update()
    {
        if (tutorialActive)
        {
            StartTutorial();

            //Check is the player is hovering over any rotation buttons
            for (int i = 0; i < cameraRot.Length; i++)
            {
                if (cameraRot[i].startTimeCount)
                {
                    CheckTutorialPart(0);
                }
            }

            //Check if player is pressing on the ground
            if(nav.arrow.activeSelf && Input.GetMouseButton(0))
            {
                CheckTutorialPart(1);
            }

            for (int i = 0; i < camZoom.Length; i++)
            {
                //Check if player is zooming in on an inspectable
                if (camZoom[i].zoomedInOn && camZoom[i].isSelected)
                {
                    CheckTutorialPart(3);
                }
                if (tutorialParts[3] && !camZoom[i].zoomedInOn && camZoom[i].isSelected)
                {
                    CheckTutorialPart(4);
                }
            }
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
                    popUpId = n;
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

    public void EnableNav()
    {
        nav.enabled = true;
    }
}



//have arrows for moving to the left or right on the screen
//a text pop-up for moving to arrow => which is already highlighted
//an arrow pointing to an object you can interact with 
//a highlight over the task ui with a popup saying what's it for


//get the onbutton hover and check if starttime Count is true
//check if arrow is visible and you clicked


//look at pamphlet
//look at task
