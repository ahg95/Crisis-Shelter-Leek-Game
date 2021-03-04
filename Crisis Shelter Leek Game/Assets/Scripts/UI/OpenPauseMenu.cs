using UnityEngine;

public class OpenPauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Transform pauseMenu = transform.GetChild(0).transform;
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;
            pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);

            foreach(Transform child in pauseMenu)
            {
                child.gameObject.SetActive(false);
            }
            pauseMenu.GetChild(0).gameObject.SetActive(true);
            pauseMenu.GetChild(1).gameObject.SetActive(true);
        }
    }
}
