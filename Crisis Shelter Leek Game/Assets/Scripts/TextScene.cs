using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextScene : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    private CanvasGroup textAlpha;
    [SerializeField] private string[] textArray;
    private int currentText = 0;

    [Tooltip("How quickly the text will fade in- and out")]
    [SerializeField] private float fadeSpeed = 0.05f;
    [Tooltip("The amount of seconds the text will be seen once it has been fully faded in")]
    [SerializeField] private float showTextLength = 5f;
    [Header("Scene Switching")]
    [Tooltip("Do you want to switch scene when all the text has been shown?")]
    [SerializeField] private bool switchScene = true;
    [Tooltip("Enter the name of the scene that you want to load")]
    [SerializeField] private string SceneToLoadString;

    private void Start()
    {
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        textAlpha = GetComponentInChildren<CanvasGroup>();
        textAlpha.alpha = 0;

        StartCoroutine(TextTimer(textArray[currentText]));
    }

    IEnumerator TextTimer(string text)
    {
        textComponent.text = text;

        while (textAlpha.alpha < 1) // Fade in text
        {
            textAlpha.alpha += 0.05f;
            yield return new WaitForSeconds(fadeSpeed);
        }

        yield return new WaitForSeconds(showTextLength); // wait 6 seconds after the textAlpha is 1.

        while (textAlpha.alpha > 0) // Fade out text
        {
            textAlpha.alpha -= 0.05f;
            yield return new WaitForSeconds(fadeSpeed);
        }

        currentText += 1;
        // if the array has a string in the current text int
        if (currentText < textArray.Length)
        {
            StartCoroutine(TextTimer(textArray[currentText]));
        }
        else
        {
            EndOfText();
        }
    }

    private void EndOfText()
    {
        if (switchScene)
        {
            SceneManager.LoadScene(SceneToLoadString);
        }
        else
        {
            print("end of array reached");
        }
    }

    // text > fade to 0 alpha > new Text > fade to 1 alpha >< repeat.
}
