using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskUI : MonoBehaviour
{
    [SerializeField]
    private TaskJourney taskJourney;

    [SerializeField]
    private TextMeshProUGUI taskTitle;

    [SerializeField]
    private TextMeshProUGUI taskDescription;

    // Update is called once per frame
    void Update()
    {
        taskTitle.text = taskJourney.assignedTask.title;
        taskDescription.text = taskJourney.assignedTask.description;
    }
}
