using UnityEngine;

public class Bed : Interactable
{
    public SpawnPoint._SpawnPoint point = SpawnPoint._SpawnPoint.bedBedroom;

    [SerializeField] TaskJourney taskJourney;
    [Space(10)]
    [SerializeField] Task[] goToTextSceneTasks;
    [SerializeField] Task[] progressOnSleepTasks;
    public override void InteractWith()
    {
        ProgressIfTask();
        GoToScene();
    }

    private void ProgressIfTask()
    {
        for (int i = 0; i < progressOnSleepTasks.Length; i++)
        {
            Task task = progressOnSleepTasks[i];
            if (task == taskJourney.assignedTask)
            {
                taskJourney.Progress();
            }
        }
    }

    public void GoToScene() 
    {
        string sceneToGoTo = "Wender Bedroom";

        SetPosOnSceneChange.instance.SetSpawnPoint(point);
        Transitions sceneTransition = FindObjectOfType<Transitions>();

        for (int i = 0; i < goToTextSceneTasks.Length; i++)
        {
            Task task = goToTextSceneTasks[i];
            if (task == taskJourney.assignedTask)
            {
                sceneToGoTo = "TextTransitionScene";
            }
        }
        for (int i = 0; i < goToTextSceneTasks.Length; i++)
        {
            Task task = goToTextSceneTasks[i];
            if (task == taskJourney.assignedTask)
            {
                sceneToGoTo = "TextTransitionScene";
            }
        }

        sceneTransition.LoadSceneTransitionStats(sceneToGoTo);
    }
}