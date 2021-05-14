using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpecificGameObjectGameEventListener : GameEventListener
{
    public GameObject gameObjectToListenFor;

    [SerializeField]
    public GameObjectGameEvent GameEvent;

    [SerializeField]
    public UnityEvent<GameObject> Response;

    public void OnEventRaised(GameObject argument)
    {
        if (gameObjectToListenFor != null && ReferenceEquals(gameObjectToListenFor, argument))
            Response.Invoke(argument);
    }



    private void OnEnable() => GameEvent.AddListener(this);

    private void OnDisable() => GameEvent.RemoveListener(this);
}
