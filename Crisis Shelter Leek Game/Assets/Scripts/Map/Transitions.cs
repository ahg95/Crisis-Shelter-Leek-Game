using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Transitions : MonoBehaviour
{
    [SerializeField] private Animator simpleTransition;//sets a simple transition that happens within the scene
    [SerializeField] private Animator sceneTransition;//sets the transition on scene change
    [SerializeField] private float endTransitionTime;//transition time after a scene end
    [SerializeField] private float startTransitionTime;//transition time before a scene start
    [SerializeField] private float sceneTransitionTime;//transition time for the scene transition
    [SerializeField] private GameObject transition;//the gameobject responsible for the simpleTransition

    [SerializeField] private CanvasGroup stats;
    [SerializeField] private float fadeInterval = 2f;//fade for the stats


    /// <summary>
    /// Starts a scene change with transition
    /// </summary>
    /// <param name="name"> Tell the name of the scene </param>
    public void LoadSceneTransition(string name)
    {
        StartCoroutine(LoadScene(name));
    }

    /// <summary>
    /// Starts a simple transition with statistics that can change scene
    /// </summary>
    /// <param name="name">Tell the name of the scene to switch to </param>
    /// <param name="switchScene">Tells whether to switch the scene or not</param>
    public void SimpleTransitionStats(string name)
    {
        StartCoroutine(TransitionWithStats(true, fadeInterval, true, name));
    }


    /// <summary>
    /// Switches scenes with transitions
    /// </summary>
    /// <param name="sceneName">Tells the name of the scene to switch to</param> 
    /// <returns></returns>
    IEnumerator LoadScene(string sceneName)
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
        simpleTransition.SetTrigger("Start");//starts the transition
        if (showStats)
        {
            yield return new WaitForSeconds(1);

            while (stats.alpha < 1) // under 1
            {
                fadeAmount = stats.alpha + (addedFade * Time.deltaTime); // add

                stats.alpha = fadeAmount;
                yield return null;
            }
        }

        yield return new WaitForSeconds(endTransitionTime);//sets for how long the screen is black after finishing a scene

        if(showStats)
        {
            while (stats.alpha > 0) // above 0
            {

                fadeAmount = stats.alpha - (addedFade * Time.deltaTime); // substract
                stats.alpha = fadeAmount;
                yield return null;
            }
        }
        simpleTransition.SetTrigger("End");//ends the transition

        if (switchScene)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
