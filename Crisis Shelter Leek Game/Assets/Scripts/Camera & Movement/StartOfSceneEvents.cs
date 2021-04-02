using UnityEngine;
using UnityEngine.SceneManagement;

public class StartOfSceneEvents : MonoBehaviour
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

        if (GlobalStats.currentTaskJSON != null)
        {
            Task currentTask = (Task)ScriptableObject.CreateInstance("Task");
            JsonUtility.FromJsonOverwrite(GlobalStats.currentTaskJSON, currentTask);

            GetComponent<CurrentTask>().assignedTask = currentTask;
        }

        gameObject.transform.position = firstNode.transform.transform.position;
        gameObject.transform.rotation = firstNode.transform.transform.rotation;
    }
    
}

