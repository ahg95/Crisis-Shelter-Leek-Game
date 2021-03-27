using System;
using UnityEngine;

public class Events : MonoBehaviour
{
    public static Events current;

    private void Awake()
    {
        current = this;
    }


    //this event checks if a certain button has been clicked

    public event Action<string> onButtonClicked;
    public void ButtonClicked(string id)
    {
        if (onButtonClicked != null)
        {
            onButtonClicked(id);
        }
    }

    public event Action<string> onPlaceExited;
    public void PlaceTriggerExit(string id)
    {
        if (onPlaceExited != null)
        {
            onPlaceExited(id);
        }
    }
}
