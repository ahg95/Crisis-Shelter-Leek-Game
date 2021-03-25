using UnityEngine;

public class ActionPerformedTest : MonoBehaviour
{
public void ActionPerformed(int days)
    {
        GlobalStats.IncreaseStats(days, days * 100);
    }
}
