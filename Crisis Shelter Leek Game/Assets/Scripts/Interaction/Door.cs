using UnityEngine;

public class Door : Interactable
{
    // To do: clean up position ID stuff
    public int positionId;
    public SpawnPoint._SpawnPoint spawnLocation;

    [SerializeField] private string sceneName;
    public override void InteractWith()
    {
        onInteraction.Invoke();

        GoToScene();
    }
    public void GoToScene() 
    {
        SetPosOnSceneChange.instance.currentSpawnPoint = spawnLocation;
        Transitions sceneTransition = GameObject.FindObjectOfType<Transitions>();

        sceneTransition.LoadSimpleSceneTransition(sceneName);
    }

    public void GoToScene(SpawnPoint._SpawnPoint point)
    {
        SetPosOnSceneChange.instance.currentSpawnPoint = point;

        Transitions sceneTransition = GameObject.FindObjectOfType<Transitions>();

        sceneTransition.LoadSimpleSceneTransition(sceneName);
    }
}

