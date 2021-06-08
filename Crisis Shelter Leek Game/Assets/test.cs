using UnityEngine;

public class test : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DaysPassed.IncreaseDaysZienn(100);
            FindObjectOfType<UpdateStats>().ShowStats();
            FindObjectOfType<Transitions>().LoadSceneTransitionStats("Wender Front Desk");
        }
    }
}
