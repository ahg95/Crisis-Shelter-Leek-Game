using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int daysSpentOnTasks;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void IncreaseDays(int sum)
    {
        daysSpentOnTasks += sum;
    }
}
