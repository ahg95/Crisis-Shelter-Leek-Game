using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField] private float fadeInterval = 2f;
    public bool screenIsBlack = false;
    [Header("Components")]
    [Space(20)]
    [SerializeField] private Image blackScreen;
    [SerializeField] private CanvasGroup stats;

    public void Fade(bool showStats)
    {
        StartCoroutine(Fade(!screenIsBlack, showStats, fadeInterval));
    }

    /// <summary>
    /// Make the screen fade to black
    /// </summary>
    /// <param name="fadeToBlack"></param> To tell whether you want the screen to fade to black or the opposite.
    /// <param name="showStats"></param> Tell whether you want the stats (days & cost) to be displayed.
    /// <param name="addedFade"></param> Tell with what interval the screen turns black
    /// <returns></returns>
    public IEnumerator Fade(bool fadeToBlack, bool showStats, float addedFade)
    {
        Color blackImage = blackScreen.color;
        float fadeAmount;

        // Black Screen
        if (fadeToBlack)
        {
            while (blackScreen.color.a < 1) // under 1
            {
                fadeAmount = blackImage.a + (addedFade * Time.deltaTime); // add

                blackImage.a = fadeAmount;
                blackScreen.color = blackImage;
                yield return null;
            }

            screenIsBlack = true;



        }
        else
        {
            while (blackScreen.color.a > 0) // above 0
            {
                fadeAmount = blackImage.a - (addedFade * Time.deltaTime); // subtract

                blackImage.a = fadeAmount;
                blackScreen.color = blackImage;
                yield return null;
            }

            screenIsBlack = false;
        }

        if (showStats)
        {
            while (stats.alpha < 1) // under 1
            {
                fadeAmount = stats.alpha + (addedFade * Time.deltaTime); // add

                stats.alpha = fadeAmount;
                yield return null;
            }
        }
    }

    private void Start()
    {
        print("Press A to increase amount of days/money");
        print("Press F to make the screen fade to black");
    }
}
