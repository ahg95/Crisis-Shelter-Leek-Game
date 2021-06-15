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
        SetPosOnSceneChange.SetSpawnPoint(spawnLocation);

        Transitions sceneTransition = FindObjectOfType<Transitions>();

        sceneTransition.LoadSimpleSceneTransition(sceneName);
    }
}

