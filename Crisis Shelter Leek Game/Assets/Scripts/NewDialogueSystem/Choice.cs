using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="New Choice", menuName = "Choice")]
public class Choice : ScriptableObject
{
    public UnityEvent consequence;
    public Consequences[] specialConsequence;
    public string description;
    public int[] id;
    public void ApplyConsequence()
    {
        GameObject.FindGameObjectWithTag("DCCM").GetComponent<DialogueChoicesConsequenceManager>().ApplyConsequenceToChoice(this);
        Debug.Log(id);
    }
}
