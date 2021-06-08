using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Transitions : MonoBehaviour
{
    #region Variables
    [Header("Transition Animations")]
    [SerializeField] private Animator simpleTransition;//sets a simple transition that happens within the scene or scene change
    [SerializeField] private Animator statsTransition;//the day/night cycle animation with stats
    [Tooltip("the time it takes for the scene to change after the transition animation started")]
    [SerializeField] private float sceneTransitionTime = 1f;

    [Header("Stats")]
    [SerializeField] private CanvasGroup statsCanvasGroup;
    [Tooltip("the interval for how fast the stats will fade in/out")]
    [SerializeField] private float fadeInterval = 2f;

    [SerializeField] private TaskJourney taskJourney;

    [Space(10)]
    [SerializeField] private float daySpeedMultiplier = 1.5f;
    [SerializeField] private float costsSpeedMultiplier = 1f;

    [Header("Stats Components")]
    [Space(20)]
    [SerializeField] private Text daysUI;
    [SerializeField] private Text costsUI;
    [SerializeField] private AudioSource tickPlayer;
    [Header("Stats Sounds")]
    [SerializeField] private AudioClip tickSound;
    [SerializeField] private AudioClip coinSound;

    //[HideInInspector] 
    public bool finishedUpdatingUI = false;//checks if it finished updating the days & cost text

    #endregion

    #region Functions

    /// <summary>
    /// Will start the coroutine that updates the stats on the UI
    /// </summary>
    public void ShowStats()
    {
        StartCoroutine(StatsUpdater());
    }

    /// <summary>
    /// Starts a scene change with transition
    /// </summary>
    /// <param name="name"> Tell the name of the scene </param>
    public void LoadSimpleSceneTransition(string name)
    {
        StartCoroutine(LoadScene(name));
    }

    /// <summary>
    /// Starts a simple transition with statistics that can change scene
    /// </summary>
    /// <param name="name">Tell the name of the scene to switch to </param>
    /// <param name="switchScene">Tells whether to switch the scene or not</param>
    public void LoadSceneTransitionStats(string name)
    {
        ShowStats();
        StartCoroutine(TransitionWithStats(true, fadeInterval, true, name));
    }
    #endregion

    #region Coroutines

    /// <summary>
    /// Switches scenes with transitions
    /// </summary>
    /// <param name="sceneName">Tells the name of the scene to switch to</param> 
    /// <returns></returns>
    private IEnumerator LoadScene(string sceneName)
    {
        yield return new WaitForSeconds(1f);

        simpleTransition.SetTrigger("Start");

        yield return new WaitForSeconds(sceneTransitionTime);

        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Creates a transition with stats
    /// </summary>
    /// <param name="showStats"> Tells if you want display stats or not </param>
    /// <param name="addedFade"> Tells with what interval the stats alpha show</param>
    /// <param name="switchScene"> Tell if you want to switch scenes </param>
    /// <param name="sceneName"> Tell what scene to switch to </param>
    /// <returns></returns>
    public IEnumerator TransitionWithStats(bool showStats, float addedFade, bool switchScene, string sceneName)
    {
        float fadeAmount;
        //simpleTransition.SetTrigger("Start");//starts the transition
        statsTransition.SetBool("StartAnim", true);//starts the transition

        if (showStats)
        {
            yield return new WaitForSeconds(1);

            while (statsCanvasGroup.alpha < 1) // under 1
            {
                fadeAmount = statsCanvasGroup.alpha + (addedFade * Time.deltaTime); // add

                statsCanvasGroup.alpha = fadeAmount;
                yield return null;
            }
        }
        yield return new WaitUntil(() => finishedUpdatingUI);//waits until the stats have finished showing
        yield return new WaitForSeconds(1.5f);//waits for a bit more sec before switching scenes
        //yield return new WaitUntil(() => Input.GetMouseButtonDown(0));//waits until the player clicked the mouse 


        if (showStats)
        {
            while (statsCanvasGroup.alpha > 0) // above 0
            {
                fadeAmount = statsCanvasGroup.alpha - (addedFade * Time.deltaTime); // substract
                statsCanvasGroup.alpha = fadeAmount;
                yield return null;
            }
        }
        //simpleTransition.SetTrigger("End");//ends the transition
        if (!switchScene)
        {
            statsTransition.SetBool("StartAnim", false);//ends the transition
        }

        if (switchScene)
        {
            statsTransition.SetBool("StartAnim", false);//ends the transition
            yield return new WaitForSeconds(1.5f);//waits for a bit more sec before switching scenes
            SceneManager.LoadScene(sceneName);
        }
    }

    /// <summary>
    /// An int and a float keep up what the costs and amount of days on screen are.
    /// It is checked whether the currently shown amount of days and costs are still below the new values.
    /// If true, the displayed amounts are increased, and the process repeats itself until it's up-to-date.
    /// </summary>
    public IEnumerator StatsUpdater()
    {
        yield return new WaitForSeconds(1.5f);

        int displayedAmountOfDays = taskJourney.CurrentAmountOfDaysAtWender;
        int newAmountOfDays = taskJourney.DaysSpentAfterProgression;

        while (displayedAmountOfDays < newAmountOfDays)
        {
            if (!tickPlayer.isPlaying) // To prevent 'spamming' of coinsounds.
            {
                tickPlayer.PlayOneShot(tickSound, 0.75f);
            }
            displayedAmountOfDays++; //Increment the display score by 
            daysUI.text = displayedAmountOfDays.ToString(); //Write it to the UI
            yield return new WaitForSeconds(1f / newAmountOfDays * daySpeedMultiplier);  // The time it takes for the count to be done should be about the same every time.
        }

        int displayedCost = taskJourney.CurrentAmountOfDaysAtWender * 100;
        int newCost = taskJourney.DaysSpentAfterProgression * 100;

        while (displayedCost < newCost)
        {
            if (!tickPlayer.isPlaying) // To prevent 'spamming' of coinsounds.
            {
                tickPlayer.PlayOneShot(coinSound, 0.35f);
            }
            displayedCost += 25; //Increment the display score by 1
            costsUI.text = displayedCost.ToString(); //Write it to the UI

            //check if the UI has been updated completely
            if (displayedAmountOfDays == newAmountOfDays && displayedCost == newCost)
            {
                finishedUpdatingUI = true;
            }

            yield return new WaitForSeconds(1f / newCost * costsSpeedMultiplier);
        }
    }
    #endregion
}
