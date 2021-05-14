using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameObjectUnityEvent : UnityEvent<GameObject>
{

}

public  class GameObjectGameEventListener : MonoBehaviour
{
    [SerializeField]
    public GameObjectGameEvent GameEvent;

    [SerializeField]
    public GameObjectUnityEvent Response;

    private void OnEnable() => GameEvent.AddListener(this);

    private void OnDisable() => GameEvent.RemoveListener(this);

    public virtual void OnEventRaised(GameObject argument) => Response.Invoke(argument);
}