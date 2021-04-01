using UnityEngine;
using UnityEngine.UI;

public class TaskCompleted : MonoBehaviour
{
    [SerializeField] private Image backgroundColor;
    [SerializeField] private Image checkmark;

    public void CheckTaskCompleted(bool completed)
    {
        backgroundColor.color = completed ? new Color(44, 255, 38) : Color.white;
    }
}
