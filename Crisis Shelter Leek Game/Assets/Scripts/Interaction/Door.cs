using UnityEngine;

public class Door : Interactable
{
    public SpawnPoint._SpawnPoint spawnLocation;

    [SerializeField] private string sceneName;
    public override void InteractWith()
    {
        onInteraction.Invoke();

        GoToScene();
    }
    public void GoToScene() 
    {
        SetPosOnSceneChange.instance.SetSpawnPoint(spawnLocation);

        print(SetPosOnSceneChange.instance.currentSpawnPoint);
        Transitions sceneTransition = FindObjectOfType<Transitions>();

        sceneTransition.LoadSimpleSceneTransition(sceneName);
    }
}

