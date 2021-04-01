using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField]
    private TaskGiver taskgiver;

void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            taskgiver.AssignTasks();
        }
    }
}
