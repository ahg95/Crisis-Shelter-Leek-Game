using UnityEngine.SceneManagement;

public static class NextScene
{
    public static void GetNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
