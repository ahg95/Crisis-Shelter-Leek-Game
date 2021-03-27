using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Click1 : Task
{
    public int requiredAmount = 3;

    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text daysText;
    public GameObject check;


    void Start()
    {
        titleText = GameObject.Find("title (1)").GetComponent<TMP_Text>();
        descriptionText = GameObject.Find("descripion (1)").GetComponent<TMP_Text>();
        daysText = GameObject.Find("days (1)").GetComponent<TMP_Text>();
        check = GameObject.Find("Fill1");

        title = "Let the clicking begin";
        description = "Click button one " + requiredAmount.ToString() + "times.";
        daysAmount = 10;
        check.GetComponent<Image>().enabled = false;

        //adds a new type of ClickingGoal to the Goals
        Goals.Add(new ClickingGoal(this, "1", "Click button one", false, 0, requiredAmount));
        Goals.ForEach(g => g.Init());

        titleText.text = title;
        descriptionText.text = description;
        daysText.text = daysAmount.ToString() + " days";

    }

    void Update()
    {
        if (taskCompleted)
        {
            check.GetComponent<Image>().enabled = true;
        }
    }
}
