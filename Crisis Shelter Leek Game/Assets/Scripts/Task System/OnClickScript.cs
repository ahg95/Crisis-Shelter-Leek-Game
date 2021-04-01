using UnityEngine;
using UnityEngine.UI;

public class OnClickScript: MonoBehaviour
{
    private Button thisButton;

    [SerializeField] private Task taskToPerform; // drag in the task to (try) perform

    private void Awake()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(OnClickButton); //subscribe
    }

    private void OnClickButton()
    {
        //PerformTask.TryPerformTask(taskToPerform);
    }
}