using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ArgsGameEventListener<T> : MonoBehaviour
{
    public ArgsGameEvent<T> GameEvent;

    public UnityEvent<T> Response;

    private void OnEnable() => GameEvent.AddListener(this);

    private void OnDisable() => GameEvent.RemoveListener(this);

    public void OnEventRaised(T argument) => Response.Invoke(argument);
}
