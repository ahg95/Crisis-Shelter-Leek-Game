using UnityEngine;
public class OpenPauseMenu : MonoBehaviour
{
    private bool isPaused = false;

    /// <summary>
    /// Invokes an animation which enabled the PauseMenu Canvas and show sit through a fade.
    /// </summary>
    public void OpenMenu()
    {
        isPaused = !isPaused;
        GetComponent<Animator>().SetBool("Open", isPaused);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu();
        }
    }
}
