using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceController : MonoBehaviour
{
    public string id;

    private void Start()
    {
        Events.current.onButtonClicked += SayPlaceEntered;
    }

    public void SayPlaceEntered(string id)
    {
        if (id == this.id)
        {
            Debug.Log("A place has been entered");
        }
    }
}
