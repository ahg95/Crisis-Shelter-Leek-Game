using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] TaskJourney taskJourney;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            taskJourney.AddDaysSpent(1);
            FindObjectOfType<Transitions>().LoadSceneTransitionStats("Wender Front Desk");
        }
    }
}
