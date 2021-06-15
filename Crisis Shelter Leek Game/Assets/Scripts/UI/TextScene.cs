using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextScene : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    private CanvasGroup textAlpha;

    [SerializeField] private TaskJourney taskJourney;
    [SerializeField] private Transitions transitions;
    [SerializeField] private TransitionTextAssociatedToTask[] transitionTextAssociatedToTask;
    private TransitionTextAssociatedToTask currentTransitionText;

    private string[] textArray;
    private int currentTextInt = 0;
    private bool isTransitioning = false;

    [Tooltip("How quickly the text will fade in- and out")]
    [SerializeField] private float fadeSpeed = 0.05f;
    [Header("Scene Switching")]
    [Tooltip("Do you want to switch scene when all the text has been shown?")]
    [SerializeField] private bool switchScene = true;

    private void Start()
    {
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        textAlpha = GetComponentInChildren<CanvasGroup>();
        textAlpha.alpha = 0;
        // Get current task and associated transition text
        foreach (TransitionTextAssociatedToTask transText in transitionTextAssociatedToTask)
        {
            if (transText.associatedTask == taskJourney.assignedTask)
            {
                currentTransitionText = transText;
                textArray = transText.textArray;
                break;
            }
        }

        StartCoroutine(TextTimer(textArray[currentTextInt]));
    }

    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && !isTransitioning)
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
        isTransitioning = true;

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

        isTransitioning = false;
    }


    private void EndOfText()
    {
        if (switchScene)
        {
            StartCoroutine(FadeOutAndLoadScene());
        }

        IEnumerator FadeOutAndLoadScene()
        {
            isTransitioning = true;

            while (textAlpha.alpha > 0) // Fade out text
            {
                textAlpha.alpha -= 0.05f;
                yield return new WaitForSeconds(fadeSpeed);
            }

            isTransitioning = false;

            yield return new WaitForSeconds(2f);

            if (currentTransitionText.progressAfterText) taskJourney.Progress();
            SetPosOnSceneChange.currentSpawnPoint = currentTransitionText.spawnPoint;
            if (!currentTransitionText.TransitionWithStats)
            {
                SceneManager.LoadScene(currentTransitionText.SceneToTransferTo);
            }
            else
            {
                transitions.LoadSceneTransitionStats(currentTransitionText.SceneToTransferTo);
            }
        }
    }

    // text > fade to 0 alpha > new Text > fade to 1 alpha >< repeat.
}
