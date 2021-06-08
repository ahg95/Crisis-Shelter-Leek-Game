using UnityEngine;

[CreateAssetMenu(fileName = "New Task", menuName = "Tasks/New Task")]
public class Task : ScriptableObject
{
    public string title;
    [TextArea(1, 2)]
    public string description;
    [Space(5)]
    [Tooltip("Amount of days it took to get to the next task")]
    public int amountOfDays;
}
