using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueChoicesConsequenceManager : MonoBehaviour
{
    public UnityEvent[] ChoiceConsequences;//on dialogue choice clicked this unity event is triggered

    /// <summary>
    /// apply consequence to the dialogue choice
    /// </summary>
    /// <param name="dialogueChoice"> the dialogues choice to which the consequence is applied</param>
    public void ApplyConsequenceToChoice(Choice dialogueChoice)
    {
        for (int c = 0; c < ChoiceConsequences.Length; c++)
        {
            for (int i = 0; i < dialogueChoice.id.Length; i++)
            {
                if (dialogueChoice.id[i] == c) { ChoiceConsequences[c].Invoke(); }
            }
        }
    }
}
