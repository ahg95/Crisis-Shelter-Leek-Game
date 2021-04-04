using UnityEngine;

public class ActionPerformedTest : MonoBehaviour
{
public void ActionPerformed(int days)
    {
        GlobalStats.IncreaseStatsManual(days, days * 100);
    }
}
