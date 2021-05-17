using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToStats : MonoBehaviour
{
    public int days;
    public float cost;
    private void Update()
    {
        days = GlobalStats.newAmountOfDays;
        cost = GlobalStats.newCost;
    }
    public void addDays (int days)
    {
        GlobalStats.IncreaseDaysZienn(days);
    }
}
