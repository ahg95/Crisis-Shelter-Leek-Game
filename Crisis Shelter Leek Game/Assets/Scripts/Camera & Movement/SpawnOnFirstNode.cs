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
        GameObject firstNode = GameObject.FindGameObjectWithTag("Respawn");
        firstNode.transform.GetComponent<MovementNode>().InteractWith();

        gameObject.transform.position = firstNode.transform.transform.position;
        gameObject.transform.rotation = firstNode.transform.transform.rotation;
    }
    
}

