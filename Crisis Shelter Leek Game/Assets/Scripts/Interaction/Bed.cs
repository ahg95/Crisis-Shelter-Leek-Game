using UnityEngine;

public class Bed : Interactable
{
    public SpawnPoint._SpawnPoint point = SpawnPoint._SpawnPoint.bedBedroom;

    [SerializeField] TaskJourney taskJourney;
    [Space(10)]
    [SerializeField] Task[] goToTextSceneTasks;
    public override void InteractWith()
    {
        onInteraction.Invoke();

        GoToScene();
    }
    public void GoToScene() 
    {
        string sceneToGoTo = "Wender Bedroom";

        SetPosOnSceneChange.instance.currentSpawnPoint = point;
        Transitions sceneTransition = GameObject.FindObjectOfType<Transitions>();

        for (int i = 0; i < goToTextSceneTasks.Length; i++)
        {
            Task task = goToTextSceneTasks[i];
            if (task == taskJourney.assignedTask)
            {
                sceneToGoTo = "TextTransitionScene";
            }
        }

        sceneTransition.LoadSimpleSceneTransition(sceneToGoTo);
    }
}