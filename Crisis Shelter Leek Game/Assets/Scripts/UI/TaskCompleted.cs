using UnityEngine;
using UnityEngine.UI;

public class TaskCompleted : MonoBehaviour
{
    [SerializeField] private Image backgroundColor;
    [SerializeField] private Image checkmark;

    public void CheckTaskCompleted(bool completed)
    {
        backgroundColor.color = completed ? Color.green : Color.white;
        checkmark.enabled = completed;
    }
}
