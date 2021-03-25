using UnityEngine;

public class GlobalStats : MonoBehaviour
{
    // Start value
    public static int startAmountOfDays = 0;
    public static int costAtStart = 0;

    // New value
    public static int newAmountOfDays = 0;
    public static float newCost = 0f;

    public static void IncreaseStats(int days, float cost)
    {
        newAmountOfDays += days;
        newCost += cost;
    }
}
