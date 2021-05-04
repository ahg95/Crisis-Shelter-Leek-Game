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

    /// <summary>
    /// Increase the amount of days stat at Zienn. The startAmount Of Days is first to start the counting at the amount of days the player last saw. then the new amount of days are added onto that.
    /// </summary>
    /// <param name="days"></param>
    public static void IncreaseDaysZienn(int days)
    {
        startAmountOfDays = newAmountOfDays;

        newAmountOfDays += days;
        newCost += newAmountOfDays * 100;
    }
    /// <summary>
    /// Save the player's current task as a JSON string to transfer it to the next scene, where it is reapplied to the player.
    /// </summary>
    /// <param name="task"></param>
    public static void SaveTask(Task task)
    {
        currentTaskJSON = JsonUtility.ToJson(task);
        currentTaskTitle = task.name;
    }
}
