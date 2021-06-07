using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UpdateStats : MonoBehaviour
{
    private int displayedAmountOfDays = DaysPassed.startAmountOfDays;
    private float displayedAmountOfMoney = DaysPassed.costAtStart;
    [SerializeField] private float daySpeedMultiplier = 1.5f;
    [SerializeField] private float costsSpeedMultiplier = 1f;
    [SerializeField] private GameObject transition;

    [Header("Components")]
    [Space(20)]
    [SerializeField] private Text daysUI;
    [SerializeField] private Text costsUI;
    [SerializeField] private AudioSource tickPlayer;
    [Header("Sounds")]
    [SerializeField] private AudioClip tickSound;
    [SerializeField] private AudioClip coinSound;

    public bool finishedStatUpdate = false;//checks if it has finished showing the days & cost
    public void ShowStats()
    {
        StartCoroutine(StatsUpdater());
    }

    /// <summary>
    /// An int and a float keep up what the costs and amount of days on screen are.
    /// It is checked whether the currently shown amount of days and costs are still below the new values.
    /// If true, the displayed amounts are increased, and the process repeats itself until it's up-to-date.
    /// </summary>
    public IEnumerator StatsUpdater()
    {
        finishedStatUpdate = false;

        yield return new WaitForSeconds(1.5f);

        while (displayedAmountOfDays < DaysPassed.newAmountOfDays)
        {
            if (!tickPlayer.isPlaying) // To prevent 'spamming' of coinsounds.
            {
                tickPlayer.PlayOneShot(tickSound, 0.75f);
            }
            displayedAmountOfDays++; //Increment the display score by 
            daysUI.text = displayedAmountOfDays.ToString(); //Write it to the UI
            yield return new WaitForSeconds(1f / DaysPassed.newAmountOfDays * daySpeedMultiplier);  // The time it takes for the count to be done should be about the same every time.
        }

        while (displayedAmountOfMoney < DaysPassed.newCost)
        {
            if (!tickPlayer.isPlaying) // To prevent 'spamming' of coinsounds.
            {
                tickPlayer.PlayOneShot(coinSound, 0.35f);
            }
            displayedAmountOfMoney += 25f; //Increment the display score by 1
            costsUI.text = displayedAmountOfMoney.ToString(); //Write it to the UI
            yield return new WaitForSeconds(1f / DaysPassed.newCost * costsSpeedMultiplier);
        }

        finishedStatUpdate = true;
    }
}