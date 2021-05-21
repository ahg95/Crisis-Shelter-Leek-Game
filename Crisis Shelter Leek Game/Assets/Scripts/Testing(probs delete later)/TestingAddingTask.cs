using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingAddingTask : MonoBehaviour
{
    [SerializeField] private TaskJourney taskJourney;



    public void addTask()
    {
        taskJourney.Progress();
    }
}
