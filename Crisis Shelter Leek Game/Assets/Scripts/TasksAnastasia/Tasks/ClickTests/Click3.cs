﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Click3 : TaskAnastasia
{
    public int amount = 4;

    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text daysText;
    public GameObject check;


    // Start is called before the first frame update
    void Start()
    {
        titleText = GameObject.Find("title (3)").GetComponent<TMP_Text>();
        descriptionText = GameObject.Find("descripion (3)").GetComponent<TMP_Text>();
        daysText = GameObject.Find("days (3)").GetComponent<TMP_Text>();
        check = GameObject.Find("Fill3");

        title = "Let the clicking begin";
        description = "Click button one " + amount.ToString() + "times.";
        daysAmount = 10;
        check.GetComponent<Image>().enabled = false;

        Goals.Add(new ClickingGoal(this, "3", "Click button three", false, 0, amount));
        Goals.ForEach(g => g.Init());

        titleText.text = title;
        descriptionText.text = description;
        daysText.text = daysAmount.ToString() + " days";

    }

    // Update is called once per frame
    void Update()
    {
        if (taskCompleted)
        {
             check.GetComponent<Image>().enabled = true;
        }
    }
}