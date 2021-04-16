using UnityEngine;

public class TestingThroughInputDeleteLater : MonoBehaviour
{
    public ConversationSection testConversationSection;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            testConversationSection.dialogues[0].choices[0].Consequence.Invoke();
        }
    }
}
