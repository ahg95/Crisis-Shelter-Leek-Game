using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Choice
{ 
    [TextArea(1, 1)]    
    public string choiceText;
    [Space(10)]
    public UnityEvent Consequence;
    [Space(15)]
    public ConversationSection followUpConversation;
}
