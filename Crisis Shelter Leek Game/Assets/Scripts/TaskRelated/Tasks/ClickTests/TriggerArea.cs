using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public string id;

    private void Start()
    {
        Events.current.onButtonClicked += CheckEnter;
        Events.current.onPlaceExited += CheckExit;
    }


    private void OnTriggerEnter(Collider other)
    {
        Events.current.ButtonClicked(id);
    }

    private void OnTriggerExit(Collider other)
    {
        Events.current.PlaceTriggerExit(id);
    }

    public void CheckEnter(string place)
    {
        if (place == this.id)
        {
            Debug.Log(place + " Something");
        }
    }

    public void CheckExit(string place)
    {

        if (place == this.id)
        {
            Debug.Log(place + " Nothing");
        }
    }
}
