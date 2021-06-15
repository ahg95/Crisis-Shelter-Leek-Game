using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestionTutorial : MonoBehaviour
{
    public static bool startTutorial;
    [SerializeField] private GameObject tutorial;

    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Wender Front Desk" && startTutorial)
        {
            tutorial.SetActive(true);
            tutorial.GetComponentInChildren<TutorialManager>().StartTutorial();
        }
    }

    public void SetTutorial(bool state)
    {
        startTutorial = state;
    }
}
