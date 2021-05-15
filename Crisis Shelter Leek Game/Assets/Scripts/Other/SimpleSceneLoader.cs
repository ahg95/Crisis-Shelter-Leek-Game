using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneLoader : MonoBehaviour
{
    public enum GameScene {
        Wender,
        WenderRoom,
        WenderRoomDaisy,
        Municipality
    }

    public void LoadScene(GameScene scene)
    {
        SceneManager.LoadScene(name);
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
