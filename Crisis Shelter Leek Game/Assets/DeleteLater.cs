using UnityEngine;

public class DeleteLater : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            print(GlobalStats.currentTaskJSON);
        }
    }
}
