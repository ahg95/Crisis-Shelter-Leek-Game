using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newGameObjectGameEvent", menuName = "GameEvent/GameObjectGameEvent")]
public class GameObjectGameEvent : ScriptableObject
{
    List<GameObjectGameEventListener> listeners = new List<GameObjectGameEventListener>();

    public void AddListener(GameObjectGameEventListener gameEventListener) => listeners.Add(gameEventListener);
    public void RemoveListener(GameObjectGameEventListener gameEventListener) => listeners.Remove(gameEventListener);

    public void Raise(GameObject argument)
    {
        for (int i = listeners.Count - 1; 0 <= i; i--)
            listeners[i].OnEventRaised(argument);
    }
}
