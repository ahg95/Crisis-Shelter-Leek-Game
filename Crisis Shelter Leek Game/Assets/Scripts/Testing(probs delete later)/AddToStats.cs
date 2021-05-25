using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToStats : MonoBehaviour
{
    public int days;
    public float cost;
    private void Update()
    {
        days = DaysPassed.newAmountOfDays;
        cost = DaysPassed.newCost;
    }
    public void addDays (int days)
    {
        DaysPassed.IncreaseDaysZienn(days);
    }
}
