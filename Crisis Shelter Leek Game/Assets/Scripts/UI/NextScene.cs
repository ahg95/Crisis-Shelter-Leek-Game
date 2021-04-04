using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public void GetNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
