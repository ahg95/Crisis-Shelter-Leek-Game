using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TutorialManager : MonoBehaviour
{
    #region Variables
    [Header ("Tutorial Parts")]
    [SerializeField] private GameObject[] popUps; //the key tutorial objects
    [SerializeField] private Animator[] rotationAnim; //popUp Animations
    [SerializeField] private float[] waitTime; //how much you have to wait until you disable the object again(disable after the end animation has finished)

    [SerializeField] private int popUpId; // the id for every tutorial part

    //tutorial parts checks
    [SerializeField] private bool[] tutorialParts;

    private bool tutorialActive = false;

    //tutorial dialogue
    [Header("Assignables")]
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private GameObject skipTutorialButton;
    [SerializeField] private GameObject finishDialogueButton;

    //tutorial components
    private OnButtonHover[] cameraRot;
    private Navigation nav;
    private InteractWith camZoom;

    private QuestionTutorial askTutorial;

    #endregion

    #region Functions
    void Start()
    {
        skipTutorialButton.SetActive(false);
        finishDialogueButton.SetActive(false);

        foreach (GameObject popUp in popUps)
        {
            popUp.SetActive(false);
        }

        dialogManager = gameObject.GetComponentInChildren<DialogManager>();
        cameraRot = GameObject.FindWithTag("Player").GetComponentsInChildren<OnButtonHover>();
        nav = GameObject.FindWithTag("Player").GetComponent<Navigation>();
        camZoom = GameObject.FindGameObjectWithTag("Player").GetComponent<InteractWith>();
        askTutorial = GameObject.FindObjectOfType<QuestionTutorial>();

        //Start the tutorial when the scene has loaded
        DialogueTrigger startDialogue = GetComponent<DialogueTrigger>();
        startDialogue.TriggerDialogue();
        StartTutorial();
        CheckTutorial();
    }

    public void StartTutorial()
    {
        skipTutorialButton.SetActive(true);
        tutorialActive = true;
        StartCoroutine("TutorialStart");
    }

    public void SkipTutorial()
    {
        popUpId = popUps.Length;
        dialogManager.EndDialogue();
        skipTutorialButton.SetActive(false);
        StopCoroutine("CheckIfCompletedTutorialParts");
        askTutorial.SetTutorial(false);
    }

    /// <summary>
    /// This function will be called when the player succeded at accomplishing a tutorial part
    /// </summary>
    /// <param name="partNr"> The tutorial part id that you want to accomplish </param>
    public void CompleteTutorialPart(int partNr)
    {
        if (tutorialActive)
        {
            tutorialParts[partNr] = true;
        }
    }
    /// <summary>
    /// This function calls the coroutine that checks if some parts of the tutorial have been completed
    /// </summary>
    public void CheckTutorial()
    {
        StartCoroutine("CheckIfCompletedTutorialParts");
    }

    //Check is the player is hovering over any rotation buttons(first tutorial part)
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

    //Check if clicking on the bell
    private bool CheckBellClick()
    {
        bool clicked = false;
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.name == "Bell")
                {
                    clicked = true;
                }
            }
        }
        return clicked;
    }
    #endregion

    #region Coroutines

    /// <summary>
    /// As long as the tutorial started and hasn't finished keep looping through the tutorial parts
    /// </summary>
    /// <returns></returns>
    IEnumerator TutorialStart()
    {
        for (int i = 0; i < popUps.Length; i++)//when a tutorial part is showing the other parts are inactive
        {
            if (i == popUpId) { popUps[i].SetActive(true); }
            else { popUps[i].SetActive(false); }
        }

        if (popUpId < popUps.Length)
        {
            if (tutorialParts[popUpId])//whenever you accomplish a tutorial part move on to the next one
            {
                rotationAnim[popUpId].SetBool("animOff", true);

                yield return new WaitForSeconds(waitTime[popUpId]);
                popUpId++;
                dialogManager.DisplayNextSentence();
            }
        }
        else if (popUpId == popUps.Length)
        {
            finishDialogueButton.SetActive(true);
            skipTutorialButton.SetActive(false);
            StopCoroutine("TutorialStart");
            askTutorial.SetTutorial(false);
        }

        yield return null;
        StartCoroutine("TutorialStart");
    }

    IEnumerator CheckIfCompletedTutorialParts()
    {
        yield return new WaitForSeconds(1.5f);
        yield return new WaitUntil(() => CheckButtonHover());
        CompleteTutorialPart(0);

        //Check if player is pressing on the ground
        yield return new WaitUntil(() => nav.arrow.activeSelf && Input.GetMouseButton(0));
        CompleteTutorialPart(1);

        yield return new WaitUntil(() => CheckBellClick());
        CompleteTutorialPart(2);

        yield return new WaitUntil(() => camZoom.isZoomedIn);
        CompleteTutorialPart(3);

        yield return new WaitForSeconds(1.5f);
        yield return new WaitUntil(() => !camZoom.isZoomedIn);
        CompleteTutorialPart(4);
    }

    #endregion

}
