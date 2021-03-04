using UnityEngine;

public class ExitApp : MonoBehaviour
{
    public void Quit()
    {
        if (!Application.isEditor)
        {
            Application.Quit();
        }
        else
        {
            UnityEditor.EditorApplication.ExitPlaymode();
        }
    }
}
