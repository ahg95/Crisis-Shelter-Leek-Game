using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    /// <summary>
    /// Go to next scene.
    /// * MUST have scenes added in Build Settings.
    /// </summary>
public void GoToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
