using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [Space(10)]
    [Tooltip("Enter the name of the scene you want to change to. Names found in the array above.")]  
    [SerializeField] string sceneToChangeTo;
    [Space(10)]
    
    [SerializeField] bool updateSceneList = false;
    [Tooltip("Copy the string from this list to the scene you want to transfer to")]
    [SerializeField] List<string> currentBuildScenesReadOnly;
    public void ChangeScene()
    {
        if (sceneToChangeTo.Length > 0)
        {
            SceneManager.LoadScene(sceneToChangeTo);
            Debug.Log("Changed scene!");
        }
        else
        {
            Debug.LogWarning("You haven't entered a scene to transfer to in the Next Scene Component!");
        }
    }

    private void OnValidate()
    {
        if (updateSceneList) updateSceneList = false;

        currentBuildScenesReadOnly = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                currentBuildScenesReadOnly.Add(System.IO.Path.GetFileNameWithoutExtension(scene.path.ToString()));
            }
        }
    }
}
