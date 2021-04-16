using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ChoiceTest
{ 
    [TextArea(1, 1)]    
    public string choiceText;
    [Space(10)]
    public UnityEvent Consequence;
}
