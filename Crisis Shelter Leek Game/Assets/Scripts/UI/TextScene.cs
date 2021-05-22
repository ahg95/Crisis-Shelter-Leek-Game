using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TextScene : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    private CanvasGroup textAlpha;

    [SerializeField] private TaskJourney taskJourney;
    [SerializeField] private TransitionTextAssociatedToTask[] transitionTextAssociatedToTask;

    private string[] textArray;
    private int currentTextInt = 0;

    [Tooltip("How quickly the text will fade in- and out")]
    [SerializeField] private float fadeSpeed = 0.05f;
    [Tooltip("The amount of seconds the text will be seen once it has been fully faded in")]
    [SerializeField] private float showTextLength = 5f;
    [Header("Scene Switching")]
    [Tooltip("Do you want to switch scene when all the text has been shown?")]
    [SerializeField] private bool switchScene = true;
    private enum scenes { _MapOverview, HousingCorporation, Municipality, Zienn, ZiennFromRoom };
    private string sceneToLoadString;
    [SerializeField] private scenes sceneToLoad;

    public UnityEvent afterSceneVoid;

    private void Start()
    {
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        textAlpha = GetComponentInChildren<CanvasGroup>();
        textAlpha.alpha = 0;
        sceneToLoadString = sceneToLoad.ToString();

        // Get current task and associated transition text
        foreach (TransitionTextAssociatedToTask transText in transitionTextAssociatedToTask)
        {
            if (transText.associatedTask == taskJourney.assignedTask)
            {
                textArray = transText.textArray;
                break;
            }
        }

        StartCoroutine(TextTimer(textArray[currentTextInt]));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            currentTextInt += 1;

            ShowNextText();
        }
    }
    void ShowNextText()
    {
        if (currentTextInt < textArray.Length)
        {
            StartCoroutine(TextTimer(textArray[currentTextInt]));
        }
        else
        {
            EndOfText();
        }
    }
    IEnumerator TextTimer(string text)
    {
        // if the next current text int is still below the length

            while (textAlpha.alpha > 0) // Fade out text
            {
                textAlpha.alpha -= 0.05f;
                yield return new WaitForSeconds(fadeSpeed);
            }

            textComponent.text = text;

            while (textAlpha.alpha < 1) // Fade in text
            {
                textAlpha.alpha += 0.05f;
                yield return new WaitForSeconds(fadeSpeed);
            }

            yield return new WaitForSeconds(showTextLength); // wait 6 seconds after the textAlpha is 1.

        /*        // if the array has a string in the current text int
                if (currentText < textArray.Length)
                {
                    StartCoroutine(TextTimer(textArray[currentText]));
                }
                else
                {
                    EndOfText();
                }*/
    }

    private void EndOfText()
    {
        afterSceneVoid.Invoke();

        if (switchScene)
        {
            SceneManager.LoadScene(sceneToLoadString);
        }
        else
        {
            // print("end of array reached");
        }
    }

    // text > fade to 0 alpha > new Text > fade to 1 alpha >< repeat.
}
