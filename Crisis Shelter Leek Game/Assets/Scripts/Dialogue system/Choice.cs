using UnityEngine;

[System.Serializable]
public class Choice
{ 
    [TextArea(1, 1)]    
    public string choiceText;
    [Space(15)]
    public ConversationSection followUpConversation;
}
