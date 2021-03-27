using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    [SerializeField] private Text days;
    [SerializeField] private Text costs;

    public void UpdateUIStats()
    {
        days.text = GlobalStats.newAmountOfDays.ToString();
        costs.text = GlobalStats.newCost.ToString();
    }
}
