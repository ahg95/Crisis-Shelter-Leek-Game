using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    [SerializeField] private Text days;
    [SerializeField] private Text costs;

    public void UpdateUIStats()
    {
        days.text = DaysPassed.newAmountOfDays.ToString();
        costs.text = DaysPassed.newCost.ToString();
    }
}
