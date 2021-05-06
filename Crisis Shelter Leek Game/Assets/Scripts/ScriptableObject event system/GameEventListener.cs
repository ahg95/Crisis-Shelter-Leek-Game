using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent GameEvent;

    public UnityEvent Response;

    private void OnEnable() => GameEvent.AddListener(this);

    private void OnDisable() => GameEvent.RemoveListener(this);

    public void OnEventRaised() => Response.Invoke();
}
