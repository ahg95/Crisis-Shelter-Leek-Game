using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnOnFirstNode : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Transform firstNode = GameObject.FindGameObjectWithTag("Respawn").transform;

        gameObject.transform.position = firstNode.position;
        gameObject.transform.rotation = firstNode.rotation;
    }
    
}

