using UnityEngine;

public class Switch : MonoBehaviour
{
    public void SwitchPower()
    {
        if (TryGetComponent(out AudioSource source))
        {
            source.enabled = !source.isActiveAndEnabled;
        }
        if (TryGetComponent(out Light light))
        {
            light.enabled = !light.isActiveAndEnabled;
        }
    }
}
