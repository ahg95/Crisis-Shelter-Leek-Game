using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SimpleSceneLoader : MonoBehaviour
{
    public UnityEvent OnSceneStarted;

    private void Start()
    {
        OnSceneStarted.Invoke();
    }

    public enum GameScene {
        Wender,
        WenderRoom,
        WenderRoomDaisy,
        Municipality
    }

    public void LoadScene(GameScene scene)
    {
        SceneManager.LoadScene(GetSceneNameForGameScene(scene));
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void MyTestFunction(int a)
    {
        Debug.Log("Test");
    }

    private string GetSceneNameForGameScene(GameScene scene)
    {
        string sceneName;

        switch (scene)
        {
            case GameScene.Wender:
                sceneName = "Wender";
                break;
            case GameScene.WenderRoom:
                sceneName = "Wender room";
                break;
            case GameScene.WenderRoomDaisy:
                sceneName = "Wender room with Daisy";
                break;
            case GameScene.Municipality:
                sceneName = "Municipality";
                break;
            default:
                sceneName = scene.ToString();
                break;
        }

        return sceneName;
    }

}
