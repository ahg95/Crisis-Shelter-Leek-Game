using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TutorialManager : MonoBehaviour
{
    #region Variables
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

    [SerializeField]private List<Interactable> targetedCamZoom;

    #endregion
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
    }

/*    private void Update()
    {
        if (tutorialActive)
        {
            StartTutorial();
        }
    }*/

    #region TutorialChecks
    //Check is the player is hovering over any rotation buttons
    private bool CheckButtonHover()
    {
        bool hovering = false;
        for (int i = 0; i < cameraRot.Length; i++)
        {
            if (cameraRot[i].startTimeCount)
            {
                hovering = true;
            }
        }
        return hovering;
    }

    //Check if player is zooming in on an inspectable
    private bool CheckZoomIn()
    {
        bool zooming = false;
        for (int i = 0; i < camZoom.Length; i++)
        {
            if (camZoom[i].zoomedInOn && camZoom[i].isSelected)
            {
                zooming = true;
                targetedCamZoom.Add(camZoom[i]);
            }
        }
        return zooming;
    }

    //Check if player is zooming out on an inspectable
    private bool CheckZoomOut()
    {
        bool zoomingOut = false;

        if (tutorialParts[3] && !targetedCamZoom[0].zoomedInOn && !targetedCamZoom[0].isSelected)
        {
            zoomingOut = true;
            targetedCamZoom.Clear();
        }
        return zoomingOut;
    }
    #endregion
    public void CheckTutorial()
    {
        StartCoroutine("CheckForTutorialParts");
    }
    IEnumerator CheckForTutorialParts()
    {
        yield return new WaitUntil(() => CheckButtonHover());
        TutorialPartAchieved(0);

        //Check if player is pressing on the ground
        yield return new WaitUntil(() => nav.arrow.activeSelf && Input.GetMouseButton(0));
        TutorialPartAchieved(1);

        yield return new WaitUntil(() => CheckZoomIn());
        TutorialPartAchieved(3);

        yield return new WaitUntil(() => CheckZoomOut());
        TutorialPartAchieved(4);

    }

    public void StartTutorial()
    {
        //StartCoroutine("TutorialStart");
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
    IEnumerator TutorialStart()
    {
        tutorialActive = true;
        skipTutorialButton.SetActive(true);

        yield return null;
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
    public void TutorialPartAchieved(int partNr)
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


//get the onbutton hover and check if starttime Count is true
//check if arrow is visible and you clicked


//look at pamphlet
//look at task
