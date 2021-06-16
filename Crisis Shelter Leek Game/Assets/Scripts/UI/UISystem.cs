using TMPro;
using UnityEngine;

public class UISystem : MonoBehaviour
{
    [SerializeField]private Animator taskUIAnim;
    [SerializeField] private GameObject taskUI;

    [Header("Text UI")]
    [SerializeField] private TextMeshProUGUI daysDataText;
    [SerializeField] private TextMeshProUGUI costDataText;
    [SerializeField] private TextMeshProUGUI taskText;

    [Space(15)]
    [SerializeField] private GameObject newTaskPopUp;
    [SerializeField] private GameObject newTaskPopUpIcon;

    [Space(15)]
    [SerializeField] private TaskJourney taskJourney;

    private void Start()
    {
        updateTaskUI();
        UpdateDaysAndCostUI();
    }

    public void onTaskClicked()
    {
        if (newTaskPopUpIcon.activeSelf)
        {
            newTaskPopUpIcon.SetActive(false);
        }

        if (taskUIAnim.GetBool("Show"))
        {
            taskUIAnim.SetBool("Show", false);
        }
        else
        {
            taskUIAnim.SetBool("Show", true);
        }
    }

    public void UpdateDaysAndCostUI()
    {
        daysDataText.text = "Days: " + taskJourney.newDays;
        // costDataText.text = "Cost: " + taskJourney.GetCosts(taskJourney.newDays);
    }

    //updates the text of the ui and shows the "new task" popup(by instantiating it and deleting after couple sec)
    public void updateTaskUI()
    {
        taskText.text = taskJourney.assignedTask.title;

        //newTaskPopUpIcon.SetActive(true);

        if (!taskUIAnim.GetBool("Show"))
        {
            ShowUI();
            taskUIAnim.SetTrigger("Pressed");
        }

        //the small icon
        GameObject taskIconClone = Instantiate(newTaskPopUpIcon);
        taskIconClone.SetActive(true);

        taskIconClone.transform.SetParent(taskUI.transform);
        taskIconClone.GetComponent<RectTransform>().sizeDelta = newTaskPopUpIcon.GetComponent<RectTransform>().sizeDelta;
        taskIconClone.GetComponent<RectTransform>().localScale = newTaskPopUpIcon.GetComponent<RectTransform>().localScale;
        taskIconClone.GetComponent<RectTransform>().localPosition = newTaskPopUpIcon.GetComponent<RectTransform>().localPosition;

        Destroy(taskIconClone, 2.5f);

        /*        //the middle screen popUP
                GameObject taskPopUpClone = Instantiate(newTaskPopUp);
                taskPopUpClone.SetActive(true);

                taskPopUpClone.transform.SetParent(gameObject.transform);
                taskPopUpClone.GetComponent<RectTransform>().sizeDelta = newTaskPopUp.GetComponent<RectTransform>().sizeDelta;
                taskPopUpClone.GetComponent<RectTransform>().localScale = newTaskPopUp.GetComponent<RectTransform>().localScale;
                taskPopUpClone.GetComponent<RectTransform>().localPosition = newTaskPopUp.GetComponent<RectTransform>().localPosition;

                Destroy(taskPopUpClone, 1.5f);*/
    }
    public void ShowUI()
    {
        taskUIAnim.SetBool("Show", true);
    }
    public void HideUI()
    {
        taskUIAnim.SetBool("Show", false);
    }

    //add new task to the list
    //>instantiate a task
    //complete a task
    //>show checkmark and scratch off task
    //>the completed task would move to be the last one
    /*    public void AddTaskUI()
        {
            GameObject taskDescriptionClone = Instantiate(taskDescription);
            tasksClone.Add(taskDescriptionClone);

            taskDescriptionClone.transform.SetParent(taskUI.transform);
            taskDescriptionClone.GetComponent<RectTransform>().sizeDelta = taskDescription.GetComponent<RectTransform>().sizeDelta;
            taskDescriptionClone.GetComponent<RectTransform>().localScale = taskDescription.GetComponent<RectTransform>().localScale;

            if(tasksClone.Count > 1)
            {
                taskDescriptionClone.GetComponent<RectTransform>().localPosition = new Vector3(taskDescription.GetComponent<RectTransform>().localPosition.x, tasksClone[tasksClone.Count-1].GetComponent<RectTransform>().position.y - 900 * (tasksClone.Count - 1), taskDescription.GetComponent<RectTransform>().localPosition.z);
                print(taskDescriptionClone.GetComponent<RectTransform>().localPosition.y);

            }
            else
            {
                taskDescriptionClone.GetComponent<RectTransform>().localPosition = taskDescription.GetComponent<RectTransform>().localPosition;
                print(taskDescriptionClone.GetComponent<RectTransform>().localPosition.y);

            }

            taskDescriptionClone.SetActive(true);
        }*/
}
