using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    [SerializeField] private TaskJourney taskJourney;
    [SerializeField] private Text days;
    [SerializeField] private Text costs;

    public void UpdateUIStats()
    {
        days.text = taskJourney.CurrentAmountOfDaysAtWender.ToString();
        costs.text = taskJourney.GetCosts(taskJourney.CurrentAmountOfDaysAtWender).ToString();
    }
}
