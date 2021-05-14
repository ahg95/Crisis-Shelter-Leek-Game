using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ArgsGameEvent<T> : ScriptableObject
{
    List<ArgsGameEventListener<T>> listeners = new List<ArgsGameEventListener<T>>();

    public void AddListener(ArgsGameEventListener<T> gameEventListener) => listeners.Add(gameEventListener);
    public void RemoveListener(ArgsGameEventListener<T> gameEventListener) => listeners.Remove(gameEventListener);

    public void Raise(T argument)
    {
        for (int i = listeners.Count - 1; 0 <= i; i--)
            listeners[i].OnEventRaised(argument);
    }
}
