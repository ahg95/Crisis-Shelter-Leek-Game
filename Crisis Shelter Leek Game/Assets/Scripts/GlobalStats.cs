using UnityEngine;

public class GlobalStats : MonoBehaviour
{
    public static string currentTaskTitle;
    public static string currentTaskJSON; 
    // Start value
    public static int startAmountOfDays = 0;
    public static int costAtStart = 0;

    // New value
    public static int newAmountOfDays = 0;
    public static float newCost = 0f;

    public static void IncreaseStatsManual(int days, float cost)
    {
        newAmountOfDays += days;
        newCost += cost;
    }
    public static void IncreaseDaysZienn(int days)
    {
        newAmountOfDays += days;
        newCost += days * 100;
    }

    public static void SaveTask(Task task)
    {
        currentTaskJSON = JsonUtility.ToJson(task);
        currentTaskTitle = task.name;
    }
}
