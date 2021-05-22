using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WaitUntilVoidExecution : MonoBehaviour
{
    public UnityEvent thingsToDo;
    [SerializeField] float timeToWait = 5f;

    /// <summary>
    /// Wait the the timeToWait seconds, then execute the event.
    /// </summary>
    public void WaitAndExecute()
    {
        StartCoroutine(WaitToExecute(timeToWait));
    }

    IEnumerator WaitToExecute(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        thingsToDo.Invoke();
    }
}
