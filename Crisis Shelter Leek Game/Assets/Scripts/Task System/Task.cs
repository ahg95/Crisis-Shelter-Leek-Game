using UnityEngine;

[CreateAssetMenu(fileName = "New Task", menuName = "Tasks/New Task")]
public class Task : ScriptableObject
{
    public string title;
    [TextArea(1, 2)]
    public string description;
    public int amountOfDays;
    public Locations location;
    public enum Locations
    {
        Municipality,
        HousingCorporation,
        Wender
    };
}
