using UnityEngine;

public class OnSceneLoaded : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<UISystem>().ShowUI();
    }
}
