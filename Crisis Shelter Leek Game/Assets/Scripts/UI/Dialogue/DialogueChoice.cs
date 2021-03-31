using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueChoice
{
    public string text;
    public UnityEvent Consequence;

    //public delegate void Consequence();

    //private Consequence consequence;

        /*
    public DialogueChoice(string text, Consequence consequence)
    {
        this.text = text;
        this.consequence = consequence;
    }
    */
}
