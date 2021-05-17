using UnityEngine;

public class Door : Interactable
{
    public int positionId;
    [SerializeField] private string sceneName;
    public override void InteractWith()
    {
        onInteraction.Invoke();

        SetID();
    }
    public void SetID() 
    {
        SetPosOnSceneChange.instance.currentPositionId = positionId;

        Transitions sceneTransition = GameObject.FindObjectOfType<Transitions>();
        sceneTransition.LoadSimpleSceneTransition(sceneName);
    }
}
