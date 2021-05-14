using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpecificGameObjectGameEventListener : GameObjectGameEventListener
{
    public GameObject gameObjectToListenFor;

    public override void OnEventRaised(GameObject argument)
    {
        if (gameObjectToListenFor != null && ReferenceEquals(gameObjectToListenFor, argument))
            base.OnEventRaised(argument);
    }
}
