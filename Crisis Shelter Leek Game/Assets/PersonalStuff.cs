using UnityEngine;

public class PersonalStuff : MonoBehaviour
{
    private static bool interactedWithPersonalStuff = false;
    [SerializeField] private GameObject untidy;
    [SerializeField] private GameObject tidy;

    private void Start()
    {
        if (!interactedWithPersonalStuff)
        {
            untidy.SetActive(true);
        }
        else
        {
            tidy.SetActive(true);
        }
    }
    public void InteractWithPersonalStuff()
    {
        interactedWithPersonalStuff = true;
        untidy.SetActive(false);
        tidy.SetActive(true);
    }
}
