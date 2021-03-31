using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClicked : MonoBehaviour
{

    public string id;

    //this function is called whenever a button is being clicked
    public void ButtonClic()
    {
        Events.current.ButtonClicked(id);
    }
}
